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
    public class VideoController : Controller
    {
        private readonly DatabaseContext _context;

        public VideoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Video
        public async Task<IActionResult> Index()
        {
            string dataShowType = HttpContext.Request.Query["view"].ToString();
            ViewData["DataShowType"] = dataShowType;
            var databaseContext = _context.Videos.Include(v => v.Rockstar).Include(v => v.Tribe);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Video/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .Include(v => v.Rockstar)
                .Include(v => v.Tribe)
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // GET: Video/Create
        public IActionResult Create()
        {
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            return View();
        }

        // POST: Video/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoId,Title,Description,Link,TribeId,RockstarId")] Video video)
        {
            if (ModelState.IsValid)
            {
                video.Link = GetVideoId(video.Link);
                video.DateCreated = DateTime.Now;
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RockstarId"] = new SelectList(_context.Rockstars, "RockstarId", "RockstarId", video.RockstarId);
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", video.TribeId);
            return View(video);
        }

        // GET: Video/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RockstarNames"] = new SelectList(_context.Rockstars, "RockstarId", "Name");
            ViewBag.Link = video.Link;
            return View(video);
        }

        // POST: Video/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoId,Title,Description,Link,TribeId,RockstarId")] Video video)
        {
            if (id != video.VideoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    video.Link = GetVideoId(video.Link);
                    video.DateModified = DateTime.Now;
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.VideoId))
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
            ViewData["RockstarId"] = new SelectList(_context.Rockstars, "RockstarId", "RockstarId", video.RockstarId);
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", video.TribeId);
            return View(video);
        }

        // GET: Video/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .Include(v => v.Rockstar)
                .Include(v => v.Tribe)
                .FirstOrDefaultAsync(m => m.VideoId == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Video/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.VideoId == id);
        }

        public async Task<IActionResult> ChangeStatus(int id, bool status)
        {
            var video = new Video { VideoId = id, DatePublished = DateTime.Now, PublishedStatus = status };
            _context.Attach(video);
            if (status)
            {
                _context.Entry(video).Property(r => r.DatePublished).IsModified = true;
            }
            _context.Entry(video).Property(r => r.PublishedStatus).IsModified = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public string GetVideoId(string link)
        {
            string part1 = "";
            string part2 = "";
            if (link.Contains("youtu.be/"))
            {
                part1 = "youtu.be/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 11);
                link = result;
                return link;
            }
            else if (link.Contains("youtube.com/watch?v="))
            {
                part1 = "youtube.com/watch?v=";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 11);
                link = result;
                return link;
            }
            else if (link.Contains("youtube.com/embed/"))
            {
                part1 = "youtube.com/embed/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 11);
                link = result;
                return link;
            }
            else if (link.Contains("vimeo.com/"))
            {
                part1 = "vimeo.com/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 9);
                link = result;
                return link;
            }
            else if (link.Contains("player.vimeo.com/video/"))
            {
                part1 = "player.vimeo.com/video/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 9);
                link = result;
                return link;
            }
            else
            {
                return null;
            }
        }
    }
}
