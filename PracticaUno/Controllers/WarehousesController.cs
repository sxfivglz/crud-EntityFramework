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
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var warehouses = _context.warehouses.Include(w => w.Locations).AsQueryable();
            return View(await Pagination<Warehouses>.CreateAsync(warehouses, pageNumber ?? 1, pageSize));
            //var applicationDbContext = _context.warehouses.Include(w => w.Locations);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.warehouses
                .Include(w => w.Locations)
                .FirstOrDefaultAsync(m => m.warehouse_id == id);
            if (warehouses == null)
            {
                return NotFound();
            }

            return View(warehouses);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            ViewData["location_id"] = new SelectList(_context.locations, "location_id", "address");
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("warehouse_id,warehouse_name,location_id")] Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["location_id"] = new SelectList(_context.locations, "location_id", "address", warehouses.location_id);
            return View(warehouses);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.warehouses.FindAsync(id);
            if (warehouses == null)
            {
                return NotFound();
            }
            ViewData["location_id"] = new SelectList(_context.locations, "location_id", "address", warehouses.location_id);
            return View(warehouses);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("warehouse_id,warehouse_name,location_id")] Warehouses warehouses)
        {
            if (id != warehouses.warehouse_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehousesExists(warehouses.warehouse_id))
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
            ViewData["location_id"] = new SelectList(_context.locations, "location_id", "address", warehouses.location_id);
            return View(warehouses);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouses = await _context.warehouses
                .Include(w => w.Locations)
                .FirstOrDefaultAsync(m => m.warehouse_id == id);
            if (warehouses == null)
            {
                return NotFound();
            }

            return View(warehouses);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouses = await _context.warehouses.FindAsync(id);
            if (warehouses != null)
            {
                _context.warehouses.Remove(warehouses);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehousesExists(int id)
        {
            return _context.warehouses.Any(e => e.warehouse_id == id);
        }
    }
}
