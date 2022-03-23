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
        public async Task<IActionResult> Create([Bind("TribeId,Name,Desctription")] Tribe tribe)
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
        public async Task<IActionResult> Edit(int id, [Bind("TribeId,Name,Desctription")] Tribe tribe)
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
    }
}
