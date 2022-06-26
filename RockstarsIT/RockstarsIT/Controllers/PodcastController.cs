using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    public class PodcastController : Controller
    {
        Spotify spotify = new Spotify();
        private readonly DatabaseContext _context;

        public PodcastController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Podcast
        public async Task<IActionResult> Index()
        {
            string dataShowType = HttpContext.Request.Query["view"].ToString();
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["DataShowType"] = dataShowType;
            var databaseContext = _context.Podcasts.Include(p => p.PodcastContents);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Podcast/Create
        public IActionResult Create()
        {
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            return View();
        }

        // POST: Podcast/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PodcastId,URL,TribeId")] Podcast podcast)
        {
            if (podcast.URL.Contains("?si="))
            {
                podcast.URL = podcast.URL.Substring(0, podcast.URL.IndexOf("?"));
            }

            podcast.Title = spotify.GetShowTitle(spotify.GetSpotifyLinkId(podcast.URL));
            podcast.Description = spotify.GetShowDescription(spotify.GetSpotifyLinkId(podcast.URL));

            if (spotify.CheckShowLinkInput(podcast.URL))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(podcast);
                    await _context.SaveChangesAsync();

                    var podcastContent = new PodcastContent()
                    {
                        Title = podcast.Title,
                        Description = podcast.Description,
                        LanguageId = 1,
                        PodcastId = podcast.PodcastId
                    };
                    _context.Add(podcastContent);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                return View(podcast);
            }
            else
            {
                throw new Exception("Invalid Link");
            }
        }

        // GET: Podcast/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcasts.FindAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["PodcastContent"] = await _context.PodcastContents.Where(p => p.LanguageId == 1).FirstOrDefaultAsync(m => m.PodcastId == id);
            return View(podcast);
        }

        // POST: Podcast/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PodcastId,Title,Description,URL,TribeId")] Podcast podcast)
        {
            if (id != podcast.PodcastId)
            {
                return NotFound();
            }

            if (spotify.CheckShowLinkInput(podcast.URL))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(podcast);

                        var podcastContent = new PodcastContent()
                        {
                            PodcastContentId = podcast.PodcastContentId,
                            Title = podcast.Title,
                            Description = podcast.Description,
                            LanguageId = 1,
                            PodcastId = id
                        };
                        _context.Update(podcastContent);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PodcastExists(podcast.PodcastId))
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
            }
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            return View(podcast);
        }

        // POST: Podcast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podcast = await _context.Podcasts.FindAsync(id);
            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodcastExists(int id)
        {
            return _context.Podcasts.Any(e => e.PodcastId == id);
        }
        
        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var podcast = new Podcast { PodcastId = id, DatePublished = DateTime.Now, PublishedStatus = status };
            _context.Attach(podcast);
            if (status)
            {
                _context.Entry(podcast).Property(r => r.DatePublished).IsModified = true;
            }
            _context.Entry(podcast).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
            return Redirect("/Podcast/Index?view=grid");
        }

        public async Task<IActionResult> Translate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcast.FirstOrDefaultAsync(m => m.PodcastId == id);
            if (podcast == null)
            {
                return NotFound();
            }

            ViewData["Languages"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return View(podcast);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Translate(int id, [Bind("PodcastContentId,PodcastId,Title,Description,LanguageId")] PodcastContent podcastContent)
        {
            if (id != podcastContent.PodcastId)
            {
                return NotFound();
            }

            if (!(_context.PodcastContents.Where(v => v.LanguageId == podcastContent.LanguageId).Where(v => v.PodcastId == podcastContent.PodcastId).Count() > 0))
            {
                _context.Add(podcastContent);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Update(podcastContent);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = podcastContent.PodcastId });
        }

        public async Task<JsonResult> GetContentWithLanguage(int id, int languageId)
        {
            if (id > 0)
            {
                var podcastContent = await _context.PodcastContents.Where(p => p.LanguageId == languageId).FirstOrDefaultAsync(m => m.PodcastId == id);

                int podcastContentId = (podcastContent == null) ? 0 : podcastContent.PodcastContentId;
                string title = (podcastContent == null) ? "" : podcastContent.Title;
                string description = (podcastContent == null) ? "" : podcastContent.Description;

                return Json(new { Success = true, Title = title, Description = description, PodcastContentId = podcastContentId });
            }
            return Json(new { Succes = false, Message = "Something went wrong" });
        }
    }
}
