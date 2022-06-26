using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    [Authorize]
    public class PodcastEpisodeController : Controller
    {
        Spotify spotify = new Spotify();
        private readonly DatabaseContext _context;

        public PodcastEpisodeController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Podcast
        public async Task<IActionResult> Index(string orderBy, string orderOn, string searchWords)
        {
            var databaseContext = _context.PodcastEpisodes.Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(orderOn))
            {
                switch (orderBy)
                {
                    case "title":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.PodcastEpisodeContents.Where(v => v.LanguageId == 1).First().Title).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.PodcastEpisodeContents.Where(v => v.LanguageId == 1).First().Title).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        break;
                    case "author":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.Rockstar).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.Rockstar).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        break;
                    case "tribe":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.Tribe).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.Tribe).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        break;
                    case "datePublished":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.DatePublished).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.DatePublished).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        break;
                    case "status":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.PublishedStatus).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.PublishedStatus).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
                        }
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchWords))
            {
                databaseContext = _context.PodcastEpisodes.Where(p => p.PodcastEpisodeContents.Where(v => v.LanguageId == 1).First().Title.Contains(searchWords) || p.PodcastEpisodeContents.Where(v => v.LanguageId == 1).First().Description.Contains(searchWords) || p.Rockstar.Name.Contains(searchWords)).Include(p => p.Rockstar).Include(p => p.Tribe).Include(v => v.PodcastEpisodeContents);
            }
            string dataShowType = HttpContext.Request.Query["view"].ToString();
            ViewData["DataShowType"] = dataShowType;
            return View(await databaseContext.ToListAsync());
        }


        // GET: Podcast/Create
        public IActionResult Create()
        {
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            ViewData["PodcastTitles"] = new SelectList(_context.PodcastContents, "PodcastId", "Title");
            return View();
        }

        // POST: Podcast/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PodcastId,URL,RockstarId,TribeId")] PodcastEpisode podcastEpisode)
        {
            if (podcastEpisode.URL.Contains("?si="))
            {
                podcastEpisode.URL = podcastEpisode.URL.Substring(0, podcastEpisode.URL.IndexOf("?"));
            }
            podcastEpisode.Title = spotify.GetTitle(spotify.GetSpotifyLinkId(podcastEpisode.URL));
            podcastEpisode.Description = spotify.GetDescription(spotify.GetSpotifyLinkId(podcastEpisode.URL));

            if (spotify.CheckLinkInput(podcastEpisode.URL))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(podcastEpisode);
                    await _context.SaveChangesAsync();

                    var podcastEpisodeContent = new PodcastEpisodeContent()
                    {
                        Title = podcastEpisode.Title,
                        Description = podcastEpisode.Description,
                        LanguageId = 1,
                        PodcastEpisodeId = podcastEpisode.PodcastEpisodeId
                    };
                    _context.Add(podcastEpisodeContent);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
                ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
                ViewData["PodcastTitles"] = new SelectList(_context.PodcastContents, "PodcastId", "Title");
                return View(podcastEpisode);
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

            var podcast = await _context.PodcastEpisodes.FindAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            ViewData["PodcastEpisodeContent"] = await _context.PodcastEpisodeContents.Where(p => p.LanguageId == 1).FirstOrDefaultAsync(m => m.PodcastEpisodeId == id);
            return View(podcast);
        }

        // POST: Podcast/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PodcastEpisodeId,PodcastId,Title,Description,URL,RockstarId,TribeId")] PodcastEpisode podcastEpisode)
        {
            if (id != podcastEpisode.PodcastEpisodeId)
            {
                return NotFound();
            }

            if (spotify.CheckLinkInput(podcastEpisode.URL))
            {
                var podcast = _context.PodcastEpisodes.Find(podcastEpisode.PodcastEpisodeId);
                _context.Entry<PodcastEpisode>(podcast).State = EntityState.Detached;
                if (podcastEpisode.URL != podcast.URL)
                {
                    podcastEpisode.Title = spotify.GetTitle(spotify.GetSpotifyLinkId(podcastEpisode.URL));
                    podcastEpisode.Description = spotify.GetDescription(spotify.GetSpotifyLinkId(podcastEpisode.URL));
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(podcastEpisode);

                        var podcastEpisodeContent = new PodcastEpisodeContent()
                        {
                            PodcastEpisodeContentId = podcastEpisode.PodcastEpisodeContentId,
                            Title = podcastEpisode.Title,
                            Description = podcastEpisode.Description,
                            LanguageId = 1,
                            PodcastEpisodeId = id
                        };
                        _context.Update(podcastEpisodeContent);

                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PodcastExists(podcastEpisode.PodcastEpisodeId))
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
                ViewData["RockstarId"] = new SelectList(_context.Rockstars, "RockstarId", "RockstarId", podcastEpisode.RockstarId);
                ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", podcastEpisode.TribeId);
                return View(podcastEpisode);
            }
            else
            {
                throw new Exception("Invalid Link");
            }
        }

        // POST: Podcast/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podcast = await _context.PodcastEpisodes.FindAsync(id);
            _context.PodcastEpisodes.Remove(podcast);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodcastExists(int id)
        {
            return _context.PodcastEpisodes.Any(e => e.PodcastEpisodeId == id);
        }

        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var podcast = new PodcastEpisode { PodcastEpisodeId = id, DatePublished = DateTime.Now, PublishedStatus = status };
            _context.Attach(podcast);
            if (status)
            {
                _context.Entry(podcast).Property(r => r.DatePublished).IsModified = true;
            }
            _context.Entry(podcast).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
            return Redirect("/PodcastEpisode/Index?view=grid");
        }

        public async Task<IActionResult> Translate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.PodcastEpisodes.FirstOrDefaultAsync(m => m.PodcastEpisodeId == id);
            if (podcast == null)
            {
                return NotFound();
            }

            ViewData["Languages"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return View(podcast);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Translate(int id, [Bind("PodcastEpisodeContentId,PodcastEpisodeId,Title,Description,LanguageId")] PodcastEpisodeContent podcastEpisodeContent)
        {
            if (id != podcastEpisodeContent.PodcastEpisodeId)
            {
                return NotFound();
            }

            if (!(_context.PodcastEpisodeContents.Where(v => v.LanguageId == podcastEpisodeContent.LanguageId).Where(v => v.PodcastEpisodeId == podcastEpisodeContent.PodcastEpisodeId).Count() > 0))
            {
                _context.Add(podcastEpisodeContent);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Update(podcastEpisodeContent);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = podcastEpisodeContent.PodcastEpisodeId });
        }

        public async Task<JsonResult> GetContentWithLanguage(int id, int languageId)
        {
            if (id > 0)
            {
                var podcastEpisodeContent = await _context.PodcastEpisodeContents.Where(p => p.LanguageId == languageId).FirstOrDefaultAsync(m => m.PodcastEpisodeId == id);

                int podcastEpisodeContentId = (podcastEpisodeContent == null) ? 0 : podcastEpisodeContent.PodcastEpisodeContentId;
                string title = (podcastEpisodeContent == null) ? "" : podcastEpisodeContent.Title;
                string description = (podcastEpisodeContent == null) ? "" : podcastEpisodeContent.Description;

                return Json(new { Success = true, Title = title, Description = description, PodcastEpisodeContentId = podcastEpisodeContentId });
            }
            return Json(new { Succes = false, Message = "Something went wrong" });
        }
    }
}
