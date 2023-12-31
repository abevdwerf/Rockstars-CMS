using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    [Authorize]
    public class PodcastEpisodeController : Controller
    {
        Spotify spotify = new Spotify();
        private readonly DatabaseContext _context;
        private readonly IStringLocalizer<PodcastEpisodeController> _stringLocalizer;

        public PodcastEpisodeController(DatabaseContext context, IStringLocalizer<PodcastEpisodeController> stringLocalizer)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        // GET: Podcast
        public async Task<IActionResult> Index(string orderBy, string orderOn, string searchWords)
        {
            var databaseContext = _context.PodcastEpisodes.Include(p => p.Rockstar).Include(p => p.Tribe);
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(orderOn))
            {
                switch (orderBy)
                {
                    case "title":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.Title).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.Title).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "author":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.Rockstar).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.Rockstar).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "tribe":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.Tribe).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.Tribe).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "datePublished":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.DatePublished).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.DatePublished).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                    case "status":
                        if (orderOn == "asc")
                        {
                            databaseContext = _context.PodcastEpisodes.OrderBy(p => p.PublishedStatus).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        else
                        {
                            databaseContext = _context.PodcastEpisodes.OrderByDescending(p => p.PublishedStatus).Include(p => p.Rockstar).Include(p => p.Tribe);
                        }
                        break;
                }
            }
            if (!string.IsNullOrEmpty(searchWords))
            {
                databaseContext = _context.PodcastEpisodes.Where(p => p.Title.Contains(searchWords) || p.Description.Contains(searchWords) || p.Rockstar.Name.Contains(searchWords)).Include(p => p.Rockstar).Include(p => p.Tribe);
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
            ViewData["PodcastTitles"] = new SelectList(_context.Podcasts, "PodcastId", "Title");
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
                    return RedirectToAction(nameof(Index));
                }
                ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
                ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
                ViewData["PodcastTitles"] = new SelectList(_context.Podcasts, "PodcastId", "Title");
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
    }
}
