using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    public class RockstarsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;

        public RockstarsController(DatabaseContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        // GET: Rockstars
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Rockstars.Include(r => r.Tribe).Include(r => r.Role);
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
            ViewData["TribeNames"] = new SelectList(_context.Tribes, "TribeId", "Name");
            ViewData["RoleNames"] = new SelectList(_context.Roles, "RoleId", "Name");
            return View();
        }

        // POST: Rockstars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RockstarId,TribeId,RoleId,Chapter,LinkedIn,Description,Quote,ImageFile")] Rockstar rockstar)
        {
            if (ModelState.IsValid)
            {
                if (rockstar.ImageFile != null)
                {
                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(rockstar.ImageFile.FileName);
                    string extension = Path.GetExtension(rockstar.ImageFile.FileName);
                    rockstar.IMG = fileName = fileName + DateTime.Now.ToString("yymmsfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await rockstar.ImageFile.CopyToAsync(fileStream);
                    }
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
            ViewData["TribeId"] = new SelectList(_context.Tribes, "TribeId", "TribeId", rockstar.TribeId);
            return View(rockstar);
        }

        // POST: Rockstars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RockstarId,TribeId,Role,Chapter,LinkedIn,Description,Quote,IMG")] Rockstar rockstar)
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
