using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly string _azureConnectionString;
        private readonly IStringLocalizer<ArticleController> _stringLocalizer;

        public ArticleController(DatabaseContext context, IConfiguration configuration, IStringLocalizer<ArticleController> stringLocalizer)
        {
            _context = context;
            _azureConnectionString = configuration.GetConnectionString("ConnectionStringBlob");
            _stringLocalizer = stringLocalizer;
        }

        // GET: Article
        public async Task<IActionResult> Index(string orderBy, string orderOn, string searchWords)
        {
            string dataShowType = HttpContext.Request.Query["view"].ToString();
            ViewData["DataShowType"] = dataShowType;
            var databaseContext = _context.Article.Include(a => a.Rockstar).Include(a => a.Tribe);
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(orderOn))
            {
                switch (orderBy)
                {
                    case "title":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.Article.OrderBy(p => p.Title).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.Article.OrderByDescending(p => p.Title).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "description":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.Article.OrderBy(p => p.Description).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.Article.OrderByDescending(p => p.Description).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "rockstar":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.Article.OrderBy(p => p.Rockstar).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.Article.OrderByDescending(p => p.Rockstar).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "datePublished":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.Article.OrderBy(p => p.DatePublished).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.Article.OrderByDescending(p => p.DatePublished).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "status":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.Article.OrderBy(p => p.PublishedStatus).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.Article.OrderByDescending(p => p.PublishedStatus).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchWords))
            {
                databaseContext = _context.Article.Where(p => p.Title.Contains(searchWords) || p.Description.Contains(searchWords) || p.Rockstar.Name.Contains(searchWords)).Include(p => p.Rockstar).Include(p => p.Tribe);
            }

            return View(await databaseContext.ToListAsync());
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
            ModelState.Remove("Images");
            if (ModelState.IsValid)
            {
                _context.Add(article);
                await _context.SaveChangesAsync();

                //create ArticleTextBlock
                var articleTextBlocks = new ArticleTextBlocks()
                {
                    ArticleId = article.ArticleId
                };

                _context.Add(articleTextBlocks);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", article.TribeId);
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

            var article = await _context.Article.Include(a => a.ArticleImages).Include(a => a.ArticleTextBlocks).FirstOrDefaultAsync(m => m.ArticleId == id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArticleId,RockstarId,Title,Description")] Article article)
        {
            if (id != article.ArticleId)
            {
                return NotFound();
            }

            ModelState.Remove("Images");

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
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", article.TribeId);
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
        public async Task<ActionResult> UploadImage(Article article)
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
                        list.Add(new Tuple<int, string>(articleImages.ArticleImageId, articleImages.URL));
                    }
                    return Json(new { Success = true, ArticleImages = list, Message = "Afbeelding geüpload." });
                }
            }

            return Json(new { Success = false, Message = "Geen afbeelding." });
        }

        [HttpPost]
        public async Task<ActionResult> UploadImageEditor(IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            var list = new List<Tuple<int, string>>();

            string currentFileName = Path.GetFileNameWithoutExtension(upload.FileName);
            string extension = Path.GetExtension(upload.FileName);
            string newFileName = currentFileName + DateTime.Now.ToString("yymmsfff") + extension;

            var container = new BlobContainerClient(_azureConnectionString, "article-images");

            // Method to create a new Blob client.
            var blob = container.GetBlobClient(newFileName);

            // Create a file stream and use the UploadSync method to upload the Blob.
            using (var fileStream = upload.OpenReadStream())
            {
                await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = upload.ContentType });
            }

            var articleImages = new ArticleImages()
            {
                URL = blob.Uri.ToString()
            };

            _context.Add(articleImages);
            await _context.SaveChangesAsync();
            list.Add(new Tuple<int, string>(articleImages.ArticleImageId, articleImages.URL));
            
            return Json(new { Success = true, ArticleImages = list, Message = "Afbeelding geüpload." });
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

        [HttpPost]
        public async Task<JsonResult> AddTextblock(Article article)
        {
            if (article != null)
            {
                var articleTextBlocks = new ArticleTextBlocks()
                {
                    ArticleId = article.ArticleId
                };

                _context.Add(articleTextBlocks);
                await _context.SaveChangesAsync();
                article = await _context.Article.Include(a => a.ArticleTextBlocks).FirstOrDefaultAsync(m => m.ArticleId == article.ArticleId);

                return Json(new { Success = true, ArticleTextblockId = articleTextBlocks.ArticleTextBlockId, Message = "Textblock added" });
            }
            return Json(new { Succes = false, Message = "Something went wrong" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTextBlock(IFormCollection formcollection)
        {
            int articleTextBlockId = int.Parse(formcollection["ArticleTextBlockId"]);

            var articleTextBlock = await _context.ArticleTextBlocks.FindAsync(articleTextBlockId);
            _context.Remove(articleTextBlock);
            await _context.SaveChangesAsync();

            return Json(new { Success = true, Message = "Text deleted." });
        }

        [HttpPost]
        public async Task<JsonResult> SaveTextblock(ArticleTextBlocks articleTextBlocks)
        {
            _context.Update(articleTextBlocks);
            await _context.SaveChangesAsync();

            return Json(new { Success = true, Message = "Textblock saved." });
        }
    }
}