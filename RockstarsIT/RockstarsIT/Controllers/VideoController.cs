﻿using System;
using System.Linq;
using System.Net;
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
            var databaseContext = _context.Videos.Include(v => v.Rockstar).Include(v => v.Tribe).Include(v => v.VideoContents);
            return View(await databaseContext.ToListAsync());
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

                var videoContent = new VideoContent()
                {
                    Title = video.Title,
                    Description = video.Description,
                    LanguageId = 1,
                    VideoId = video.VideoId
                };
                _context.Add(videoContent);
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
            ViewData["VideoContent"] = await _context.VideoContents.Where(p => p.LanguageId == 1).FirstOrDefaultAsync(m => m.VideoId == id);
            ViewBag.Link = video.Link;
            return View(video);
        }

        // POST: Video/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoId,Title,Description,Link,TribeId,RockstarId,VideoContentId")] Video video)
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

                    var videoContent = new VideoContent()
                    {
                        VideoContentId = video.VideoContentId,
                        Title = video.Title,
                        Description = video.Description,
                        LanguageId = 1,
                        VideoId = id
                    };
                    _context.Update(videoContent);

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
                ViewData["RockstarId"] = new SelectList(_context.Rockstars, "RockstarId", "RockstarId", video.RockstarId);
                ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", video.TribeId);
                return RedirectToAction(nameof(Index));
            }
            
            return RedirectToAction("Edit", new { id = video.VideoId });
        }

        // POST: Video/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
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
            return Redirect("/Video/Index?view=grid");
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
            }
            else if (link.Contains("youtube.com/watch?v="))
            {
                part1 = "youtube.com/watch?v=";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 11);
                link = result;
            }
            else if (link.Contains("youtube.com/embed/"))
            {
                part1 = "youtube.com/embed/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 11);
                link = result;
            }
            else if (link.Contains("vimeo.com/"))
            {
                part1 = "vimeo.com/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 9);
                link = result;
            }
            else if (link.Contains("player.vimeo.com/video/"))
            {
                part1 = "player.vimeo.com/video/";
                int start = link.IndexOf(part1) + part1.Length;
                int end = link.IndexOf(part2, start);
                string result = link.Substring(start, 9);
                link = result;
            }
            else
            {
                return null;
            }

            if (link.Length == 11)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://www.youtube.com/watch/?v=" + link);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                request.Method = "HEAD";
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.ResponseUri.ToString().Contains("youtube.com"))
                        {
                            return link;
                        }
                        else
                        {
                            return null;
                        }
                    }
            }
            else if (link.Length == 9)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://vimeo.com/" + link);
                request.Method = "HEAD";
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.ResponseUri.ToString().Contains("vimeo.com"))
                        {
                            return link;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            
        }

        public async Task<IActionResult> Translate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos.FirstOrDefaultAsync(m => m.VideoId == id);
            if (video == null)
            {
                return NotFound();
            }

            ViewData["Languages"] = new SelectList(_context.Languages, "LanguageId", "Name");
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Translate(int id, [Bind("VideoContentId,VideoId,Title,Description,LanguageId")] VideoContent videoContent)
        {
            if (id != videoContent.VideoId)
            {
                return NotFound();
            }

            if (!(_context.VideoContents.Where(v => v.LanguageId == videoContent.LanguageId).Where(v => v.VideoId == videoContent.VideoId).Count() > 0))
            {
                _context.Add(videoContent);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Update(videoContent);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = videoContent.VideoId });
        }

        public async Task<JsonResult> GetContentWithLanguage(int id, int languageId)
        {
            if (id > 0)
            {
                var videoContent = await _context.VideoContents.Where(p => p.LanguageId == languageId).FirstOrDefaultAsync(m => m.VideoId == id);

                int videoContentId = (videoContent == null) ? 0 : videoContent.VideoContentId;
                string title = (videoContent == null) ? "" : videoContent.Title;
                string description = (videoContent == null) ? "" : videoContent.Description;

                return Json(new { Success = true, Title = title, Description = description, VideoContentId = videoContentId });
            }
            return Json(new { Succes = false, Message = "Something went wrong" });
        }
    }
}
