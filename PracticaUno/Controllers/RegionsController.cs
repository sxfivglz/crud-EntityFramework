using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticaUno.Data;
using PracticaUno.Models;

namespace PracticaUno.Controllers
{
    public class RegionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var regions = _context.regions.AsQueryable();
            return View(await Pagination<Regions>.CreateAsync(regions, pageNumber ?? 1, pageSize));
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = await _context.regions
                .FirstOrDefaultAsync(m => m.region_id == id);
            if (regions == null)
            {
                return NotFound();
            }

            return View(regions);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("region_id,region_name")] Regions regions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regions);
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = await _context.regions.FindAsync(id);
            if (regions == null)
            {
                return NotFound();
            }
            return View(regions);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("region_id,region_name")] Regions regions)
        {
            if (id != regions.region_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionsExists(regions.region_id))
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
            return View(regions);
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regions = await _context.regions
                .FirstOrDefaultAsync(m => m.region_id == id);
            if (regions == null)
            {
                return NotFound();
            }

            return View(regions);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regions = await _context.regions.FindAsync(id);
            if (regions != null)
            {
                _context.regions.Remove(regions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionsExists(int id)
        {
            return _context.regions.Any(e => e.region_id == id);
        }
    }
}
