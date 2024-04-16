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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.products.Include(p => p.Product_Categories);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var products = _context.products.Include(p => p.Product_Categories).AsQueryable();
            return View(await Pagination<Products>.CreateAsync(products, pageNumber ?? 1, pageSize));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .Include(p => p.Product_Categories)
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["category_id"] = new SelectList(_context.product_categories, "category_id", "category_name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products product)
        {
            if (ModelState.IsValid)
            {
                // Guardar el nuevo producto en la base de datos
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Si hay errores de validación, volver a mostrar el formulario con los errores
            ViewData["category_id"] = new SelectList(_context.product_categories, "category_id", "category_name", product.category_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .Include(p => p.Product_Categories)
                .FirstOrDefaultAsync(m => m.product_id == id);

            if (products == null)
            {
                return NotFound();
            }
            ViewData["category_id"] = new SelectList(_context.product_categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("product_id,Product_name,Description,Standard_cost,List_price,category_id")] Products products)
        {
            if (id != products.product_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.product_id))
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
            ViewData["category_id"] = new SelectList(_context.product_categories, "category_id", "category_name", products.category_id);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.products
                .Include(p => p.Product_Categories)
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.products.FindAsync(id);
            if (products != null)
            {
                _context.products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.products.Any(e => e.product_id == id);
        }
    }
}
