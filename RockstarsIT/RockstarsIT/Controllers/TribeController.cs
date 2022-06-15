using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class TribeController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly string _azureConnectionString;
        private readonly IStringLocalizer<TribeController> _stringLocalizer;

        public TribeController(DatabaseContext db, IConfiguration configuration, IStringLocalizer<TribeController> stringLocalizer)
        {
            _context = db;
            _azureConnectionString = configuration.GetConnectionString("ConnectionStringBlob");
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            var objCategoryList = _context.Tribes.Include(v => v.Articles).Include(v => v.Videos);
            return View(objCategoryList);
        }

        // GET: Tribe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tribe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("TribeId,Name,Description,Spotify,LeadAddress")] Tribe tribe)
        {
            ModelState.Remove("Images");
            if (ModelState.IsValid)
            {
                _context.Add(tribe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tribe);
        }

        // GET: Tribe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tribe = await _context.Tribes.Include(a => a.TribeImages).Include(a => a.TribeTextBlocks).FirstOrDefaultAsync(m => m.TribeId == id);
            if (tribe == null)
            {
                return NotFound();
            }
            return View(tribe);
        }

        // POST: Tribe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("TribeId,TagId,Name,Description,Spotify,LeadAddress,BlockNumber,ImageNumber")] Tribe tribe)
        {
            if (id != tribe.TribeId)
            {
                return NotFound();
            }

            ModelState.Remove("Images");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tribe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TribeExists(tribe.TribeId))
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
            return View(tribe);
        }

        // POST: Tribe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articles = _context.Article.ToList();
            foreach (var item in articles)
            {
                if (item.TribeId == id)
                {
                    _context.Article.Remove(item);
                }
            }
            var videos = _context.Videos.ToList();
            foreach (var item in videos)
            {
                if (item.TribeId == id)
                {
                    _context.Videos.Remove(item);
                }
            }
            var podcastEpisodes = _context.PodcastEpisodes.ToList();
            foreach (var item in podcastEpisodes)
            {
                if (item.TribeId == id)
                {
                    _context.PodcastEpisodes.Remove(item);
                }
            }
            var podcasts = _context.Podcasts.ToList();
            foreach (var item in podcasts)
            {
                if (item.TribeId == id)
                {
                    _context.Podcasts.Remove(item);
                }
            }
            var rockstars = _context.Rockstars.ToList();
            foreach (var item in rockstars)
            {
                if (item.TribeId == id)
                {
                    _context.Rockstars.Remove(item);
                }
            }
            var tribe = await _context.Tribes.FindAsync(id);
            _context.Tribes.Remove(tribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TribeExists(int id)
        {
            return _context.Tribes.Any(e => e.TribeId == id);
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage(Tribe tribe)
        {
            if (ModelState.IsValid)
            {
                if (tribe.Images != null)
                {
                    var list = new List<Tuple<int, string>>();

                    foreach (var file in tribe.Images)
                    {
                        string currentFileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName);
                        string newFileName = currentFileName + DateTime.Now.ToString("yymmsfff") + extension;

                        var container = new BlobContainerClient(_azureConnectionString, "tribe-images");

                        // Method to create a new Blob client.
                        var blob = container.GetBlobClient(newFileName);

                        // Create a file stream and use the UploadSync method to upload the Blob.
                        using (var fileStream = file.OpenReadStream())
                        {
                            await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                        }

                        var tribeImages = new TribeImages()
                        {
                            TribeId = tribe.TribeId,
                            URL = blob.Uri.ToString()
                        };

                        _context.Add(tribeImages);
                        await _context.SaveChangesAsync();
                        list.Add(new Tuple<int, string>(tribeImages.TribeImageId, tribeImages.URL));
                    }
                    return Json(new { Success = true, TribeImages = list, Message = "Afbeelding geüpload." });
                }
            }

            return Json(new { Success = false, Message = "Geen afbeelding." });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteImage(IFormCollection formcollection)
        {
            int tribeImageId = int.Parse(formcollection["TribeImageId"]);

            var tribeImage = await _context.TribeImages.FindAsync(tribeImageId);
            _context.Remove(tribeImage);
            await _context.SaveChangesAsync();

            return Json(new { Success = true, Message = "Afbeelding verwijderd." });
        }

        [HttpPost]
        public async Task<JsonResult> AddTextblock(Tribe tribe)
        {
            var tribeTextBlocks = new TribeTextBlock()
            {
                TribeId = tribe.TribeId
            };

            _context.Add(tribeTextBlocks);
            await _context.SaveChangesAsync();
            tribe = await _context.Tribes.Include(a => a.TribeTextBlocks).FirstOrDefaultAsync(m => m.TribeId == tribe.TribeId);

            return Json(new { Success = true, TribeTextblockId = tribeTextBlocks.TribeTextBlockId, Message = "Textblock added" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteTextBlock(IFormCollection formcollection)
        {
            int tribeTextBlockId = int.Parse(formcollection["TribeTextBlockId"]);

            var tribeTextBlock = await _context.TribeTextBlocks.FindAsync(tribeTextBlockId);
            _context.Remove(tribeTextBlock);
            await _context.SaveChangesAsync();

            return Json(new { Success = true, Message = "Text deleted." });
        }

        [HttpPost]
        public async Task<JsonResult> SaveTextblock(TribeTextBlock tribeTextBlock)
        {
            _context.Update(tribeTextBlock);
            await _context.SaveChangesAsync();

            return Json(new { Success = true, Message = "Textblock saved." });
        }

        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var tribe = new Tribe { TribeId = id, DatePublished = DateTime.Now, PublishedStatus = status };
            _context.Attach(tribe);
            if (status)
            {
                _context.Entry(tribe).Property(r => r.DatePublished).IsModified = true;
            }
            _context.Entry(tribe).Property(r => r.PublishedStatus).IsModified = true;
            _context.Article.Where(a => a.TribeId == id).ToList().ForEach(a => a.PublishedStatus = status);
            _context.PodcastEpisodes.Where(a => a.TribeId == id).ToList().ForEach(a => a.PublishedStatus = status);
            _context.Videos.Where(a => a.TribeId == id).ToList().ForEach(a => a.PublishedStatus = status);
            await _context.SaveChangesAsync();
            return Redirect("/Tribe/Index?view=grid");
        }

        [HttpPost]
        public async Task<JsonResult> GetTribeContent(int id)
        {
            if (id > 0)
            {
                var articles = await _context.Article.Where(a => a.TribeId == id).ToListAsync();
                var podcastsEpisodes = await _context.PodcastEpisodes.Where(a => a.TribeId == id).ToListAsync();
                var podcasts = await _context.Podcasts.Where(a => a.TribeId == id).ToListAsync();
                var videos = await _context.Videos.Where(a => a.TribeId == id).ToListAsync();

                return Json(new { Success = true, Articles = articles, Videos = videos, Podcasts = podcasts, PodcastsEpisodes = podcastsEpisodes });
            }
            else
            {
                return Json(new { Success = false, Message = "Er is iets mis gegaan." });
            }
        }

        [HttpPost]
        public async Task PublishTribeArticles(List<string> ids)
        {
            foreach (var id in ids)
            {
                _context.Article.Where(a =>  a.ArticleId == Convert.ToInt32( id)).ToList().ForEach(a => a.PublishedStatus = true);
                await _context.SaveChangesAsync();
            }
            var tribe = new Tribe { TribeId = Convert.ToInt32( ids[0]), DatePublished = DateTime.Now, PublishedStatus = true };
            _context.Attach(tribe);
            
            _context.Entry(tribe).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
        }
        [HttpPost]
        public async Task PublishTribeVideos(List<string> ids)
        {
            foreach (var id in ids)
            {
                _context.Videos.Where(a => a.VideoId == Convert.ToInt32(id)).ToList().ForEach(a => a.PublishedStatus = true);
                await _context.SaveChangesAsync();
            }
            var tribe = new Tribe { TribeId = Convert.ToInt32(ids[0]), DatePublished = DateTime.Now, PublishedStatus = true };
            _context.Attach(tribe);

            _context.Entry(tribe).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
        }
        [HttpPost]
        public async Task PublishTribePodcasts(List<string> ids)
        {
            foreach (var id in ids)
            {
                _context.Podcasts.Where(a => a.PodcastId == Convert.ToInt32(id)).ToList().ForEach(a => a.PublishedStatus = true);
                await _context.SaveChangesAsync();
            }
            var tribe = new Tribe { TribeId = Convert.ToInt32(ids[0]), DatePublished = DateTime.Now, PublishedStatus = true };
            _context.Attach(tribe);

            _context.Entry(tribe).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
        }
        [HttpPost]
        public async Task PublishTribePodcastsEpisodes(List<string> ids)
        {
            foreach (var id in ids)
            {
                _context.PodcastEpisodes.Where(a => a.PodcastEpisodeId == Convert.ToInt32(id)).ToList().ForEach(a => a.PublishedStatus = true);
                await _context.SaveChangesAsync();
            }
            var tribe = new Tribe { TribeId = Convert.ToInt32(ids[0]), DatePublished = DateTime.Now, PublishedStatus = true };
            _context.Attach(tribe);

            _context.Entry(tribe).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
