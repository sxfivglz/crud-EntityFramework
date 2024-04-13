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
    public class Product_categoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Product_categoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product_categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.product_categories.ToListAsync());
        }

        // GET: Product_categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_categories = await _context.product_categories
                .FirstOrDefaultAsync(m => m.category_id == id);
            if (product_categories == null)
            {
                return NotFound();
            }

            return View(product_categories);
        }

        // GET: Product_categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product_categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("category_id,category_name,product_count")] Product_categories product_categories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_categories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product_categories);
        }

        // GET: Product_categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_categories = await _context.product_categories.FindAsync(id);
            if (product_categories == null)
            {
                return NotFound();
            }
            return View(product_categories);
        }

        // POST: Product_categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("category_id,category_name,product_count")] Product_categories product_categories)
        {
            if (id != product_categories.category_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_categories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_categoriesExists(product_categories.category_id))
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
            return View(product_categories);
        }

        // GET: Product_categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_categories = await _context.product_categories
                .FirstOrDefaultAsync(m => m.category_id == id);
            if (product_categories == null)
            {
                return NotFound();
            }

            return View(product_categories);
        }

        // POST: Product_categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product_categories = await _context.product_categories.FindAsync(id);
            if (product_categories != null)
            {
                _context.product_categories.Remove(product_categories);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_categoriesExists(int id)
        {
            return _context.product_categories.Any(e => e.category_id == id);
        }
    }
}
