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
    public class TribeController : Controller
    {
        private readonly DatabaseContext _context;

        public TribeController(DatabaseContext db)
        {
            _context = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Tribe> objCategoryList = _context.Tribes;
            return View(objCategoryList);
        }

        // GET: Tribe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tribe = await _context.Tribes
                .FirstOrDefaultAsync(m => m.TribeId == id);
            if (tribe == null)
            {
                return NotFound();
            }

            return View(tribe);
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

        public async Task<IActionResult> Create([Bind("TribeId,TagId,Name,Description,Spotify,LeadAddress,BlockNumber,ImageNumber")] Tribe tribe)

        {
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

            var tribe = await _context.Tribes.FindAsync(id);
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

        // GET: Tribe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tribe = await _context.Tribes
                .FirstOrDefaultAsync(m => m.TribeId == id);
            if (tribe == null)
            {
                return NotFound();
            }

            return View(tribe);
        }

        // POST: Tribe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tribe = await _context.Tribes.FindAsync(id);
            _context.Tribes.Remove(tribe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TribeExists(int id)
        {
            return _context.Tribes.Any(e => e.TribeId == id);
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
            _context.Podcasts.Where(a => a.TribeId == id).ToList().ForEach(a => a.PublishedStatus = status);
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
                var podcasts = await _context.Podcasts.Where(a => a.TribeId == id).ToListAsync();
                var videos = await _context.Videos.Where(a => a.TribeId == id).ToListAsync();

                return Json(new { Success = true, Articles = articles, Podcasts = podcasts, Videos = videos });
            }
            else
            {
                return Json(new { Success = false, Message = "Er is iets mis gegaan." });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> ChangeTribeContentStstus( )
        //{
            
        //}
    }
}
