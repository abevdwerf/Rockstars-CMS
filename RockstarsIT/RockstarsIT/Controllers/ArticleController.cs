using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    public class ArticleController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly string _azureConnectionString;

        public ArticleController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _azureConnectionString = configuration.GetConnectionString("ConnectionStringBlob");
        }

        // GET: Article
        public async Task<IActionResult> Index()
        {
            string dataShowType = HttpContext.Request.Query["view"].ToString();
            ViewData["DataShowType"] = dataShowType;
            var databaseContext = _context.Article.Include(a => a.Rockstar).Include(a => a.Tribe);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Article/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Rockstar)
                .FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Article/Create
        public IActionResult Create()
        {
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArticleId,RockstarId,Title,Description,Images,Text")] Article article)
        {
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RockstarId"] = new SelectList(_context.Rockstars, "RockstarId", "RockstarId", article.RockstarId);
            return View(article);
        }

        // GET: Article/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.Include(a => a.ArticleImages).FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,RockstarId,Title,Description,Text")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RockstarId"] = new SelectList(_context.Rockstars, "RockstarId", "RockstarId", article.RockstarId);
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Article.FindAsync(id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.ArticleId == id);
        }

        [HttpPost]
        public async Task<JsonResult> UploadImage(Article article)
        {
            if (ModelState.IsValid)
            {
                if (article.Images != null)
                {
                    var list = new List<Tuple<int, string>>();

                    foreach (var file in article.Images)
                    {
                        string currentFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string newFileName = currentFileName + DateTime.Now.ToString("yymmsfff") + extension;

                        var container = new BlobContainerClient(_azureConnectionString, "article-images");

                        // Method to create a new Blob client.
                        var blob = container.GetBlobClient(newFileName);

                        // Create a file stream and use the UploadSync method to upload the Blob.
                        using (var fileStream = file.OpenReadStream())
                        {
                            await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                        }

                        var articleImages = new ArticleImages()
                        {
                            ArticleId = article.ArticleId,
                            URL = blob.Uri.ToString()
                        };
                        
                        _context.Add(articleImages);
                        await _context.SaveChangesAsync();
                        article = await _context.Article.Include(a => a.ArticleImages).FirstOrDefaultAsync(m => m.ArticleId == article.ArticleId);
                        list.Add(new Tuple<int, string>(articleImages.ArticleImageId, articleImages.URL));
                    }
                    return Json(new { Success = true, ArticleImages = list, Message = "Afbeelding geüpload." });
                }

                return Json(new { Success = false, Message = "Geen afbeelding." });
            }
            else
            {
                return Json(new { Success = false, Message = "Er is iets mis gegaan." });
            }
        }

        [HttpPost]
        public async Task<JsonResult> DeleteImage(IFormCollection formcollection)
        {
            int articleImageId = int.Parse(formcollection["ArticleImageId"]);

            var articleImage = await _context.ArticleImages.FindAsync(articleImageId);
            _context.Remove(articleImage);
            await _context.SaveChangesAsync();

            return Json(new { Success = true, Message = "Afbeelding verwijderd." });
        }

        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var article = new Article { ArticleId = id, DatePublished = DateTime.Now, PublishedStatus = status };
            _context.Attach(article);
            if (status)
            {
                _context.Entry(article).Property(r => r.DatePublished).IsModified = true;
            }
            _context.Entry(article).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
            return Redirect("/Article/Index?view=grid");
        }
    }
}
