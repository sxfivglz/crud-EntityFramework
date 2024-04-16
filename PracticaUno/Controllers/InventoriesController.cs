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
    public class InventoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var inventories = _context.inventories.Include(w => w.Warehouses).AsQueryable();
            return View(await Pagination<Inventories>.CreateAsync(inventories, pageNumber ?? 1, pageSize));
        
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.inventories
                .Include(w => w.Warehouses)
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["warehouse_id"] = new SelectList(_context.warehouses, "warehouse_id", "warehouse_name");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_id,warehouse_id,quantity")] Inventories inventories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["warehouse_id"] = new SelectList(_context.warehouses, "warehouse_id", "warehouse_name", inventories.warehouse_id);
            return View(inventories);
        }

        // GET: Inventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.inventories
                .Include(w => w.Warehouses)
                .FirstOrDefaultAsync(m => m.product_id == id);
                //.FindAsync(id);
            if (inventories == null)
            {
                return NotFound();
            }
            ViewData["warehouse_id"] = new SelectList(_context.warehouses, "warehouse_id", "warehouse_name", inventories.warehouse_id);
            return View(inventories);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("product_id,warehouse_id,quantity")] Inventories inventories)
        {
            if (id != inventories.product_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoriesExists(inventories.product_id))
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
            ViewData["warehouse_id"] = new SelectList(_context.warehouses, "warehouse_id", "warehouse_name", inventories.warehouse_id);
            return View(inventories);
        }

        // GET: Inventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventories = await _context.inventories
                .Include(w => w.Warehouses)
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (inventories == null)
            {
                return NotFound();
            }

            return View(inventories);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventories = await _context.inventories.FindAsync(id);
            if (inventories != null)
            {
                _context.inventories.Remove(inventories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoriesExists(int id)
        {
            return _context.inventories.Any(e => e.product_id == id);
        }
    }
}
