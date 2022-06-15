using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    [Authorize]
    public class RockstarsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly string _azureConnectionString;
        private readonly IStringLocalizer<RockstarsController> _stringLocalizer;

        public RockstarsController(DatabaseContext context, IConfiguration configuration, IStringLocalizer<RockstarsController> stringLocalizer)
        {
            _context = context;
            _azureConnectionString = configuration.GetConnectionString("ConnectionStringBlob");
            _stringLocalizer = stringLocalizer;
        }

        // GET: Rockstars
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Rockstars.Include(r => r.Tribe).Include(r => r.Role);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Rockstars/Create
        public IActionResult Create()
        {
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RoleNames"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View();
        }

        // POST: Rockstars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RockstarId,TribeId,RoleId,Chapter,Name,LinkedIn,Description,Quote,ImageFile")] Rockstar rockstar)
        {
            if (ModelState.IsValid)
            {
                if (rockstar.ImageFile != null)
                {
                    string currentFileName = Path.GetFileNameWithoutExtension(rockstar.ImageFile.FileName);
                    string extension = Path.GetExtension(rockstar.ImageFile.FileName);
                    string newFileName = currentFileName + DateTime.Now.ToString("yymmsfff") + extension;

                    var container = new BlobContainerClient(_azureConnectionString, "rockstar-images");

                    // Method to create a new Blob client.
                    var blob = container.GetBlobClient(newFileName);

                    // Create a file stream and use the UploadSync method to upload the Blob.
                    using (var fileStream = rockstar.ImageFile.OpenReadStream())
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = rockstar.ImageFile.ContentType });
                    }
                    rockstar.IMG = blob.Uri.ToString();
                }

                _context.Add(rockstar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", rockstar.TribeId);
            return View(rockstar);
        }

        // GET: Rockstars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rockstar = await _context.Rockstars.FindAsync(id);
            if (rockstar == null)
            {
                return NotFound();
            }
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RoleNames"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View(rockstar);
        }

        // POST: Rockstars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RockstarId,TribeId,RoleId,Chapter,Name,LinkedIn,Description,Quote,ImageFile")] Rockstar rockstar)
        {
            if (id != rockstar.RockstarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rockstar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RockstarExists(rockstar.RockstarId))
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
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", rockstar.TribeId);
            return View(rockstar);
        }

        // POST: Rockstars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var articles = _context.Article.ToList();
            foreach (var item in articles)
            {
                if (item.RockstarId == id)
                {
                    item.RockstarId = null;
                    _context.Article.Update(item);
                }
            }
            var videos = _context.Videos.ToList();
            foreach (var item in videos)
            {
                if (item.RockstarId == id)
                {
                    item.RockstarId = null;
                    _context.Videos.Update(item);
                }
            }
            var podcastEpisodes = _context.PodcastEpisodes.ToList();
            foreach (var item in podcastEpisodes)
            {
                if (item.RockstarId == id)
                {
                    item.RockstarId = null;
                    _context.PodcastEpisodes.Update(item);
                }
            }
            var rockstar = await _context.Rockstars.FindAsync(id);
            _context.Rockstars.Remove(rockstar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RockstarExists(int id)
        {
            return _context.Rockstars.Any(e => e.RockstarId == id);
        }
    }
}
