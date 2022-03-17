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
    public class RockstarsController : Controller
    {
        private readonly DatabaseContext _context;

        public RockstarsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Rockstars
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Rockstars.Include(r => r.Tribe);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Rockstars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rockstar = await _context.Rockstars
                .Include(r => r.Tribe)
                .FirstOrDefaultAsync(m => m.RockstarId == id);
            if (rockstar == null)
            {
                return NotFound();
            }

            return View(rockstar);
        }

        // GET: Rockstars/Create
        public IActionResult Create()
        {
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId");
            return View();
        }

        // POST: Rockstars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RockstarId,TribeId,Chapter,Name,LinkedIn,Description,IMG")] Rockstar rockstar)
        {
            if (ModelState.IsValid)
            {
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
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", rockstar.TribeId);
            return View(rockstar);
        }

        // POST: Rockstars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RockstarId,TribeId,Chapter,Name,LinkedIn,Description,IMG")] Rockstar rockstar)
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

        // GET: Rockstars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rockstar = await _context.Rockstars
                .Include(r => r.Tribe)
                .FirstOrDefaultAsync(m => m.RockstarId == id);
            if (rockstar == null)
            {
                return NotFound();
            }

            return View(rockstar);
        }

        // POST: Rockstars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
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
