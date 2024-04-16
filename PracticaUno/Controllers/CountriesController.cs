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
    public class CountriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var countries = _context.countries.Include(c => c.Regions).AsQueryable();
            return View(await Pagination<Countries>.CreateAsync(countries, pageNumber ?? 1, pageSize));
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.country_id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }       

        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var country = await _context.countries
        //        .Include(c => c.Regions)
        //        .FirstOrDefaultAsync(m => m.country_id.Equals(id));
        //    if (country == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(country);
        //}


        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewData["region_id"] = new SelectList(_context.regions, "region_id", "region_name");
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("country_id,country_name,region_id")] Countries countries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(countries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["region_id"] = new SelectList(_context.regions, "region_id", "region_name", countries.region_id);  
            return View(countries);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.country_id == id);  
            if (countries == null)
            {
                return NotFound();
            }
            ViewData["region_id"] = new SelectList(_context.regions, "region_id", "region_name", countries.region_id);
            return View(countries);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("country_id,country_name,region_id")] Countries countries)
        {
            if (id != countries.country_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(countries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountriesExists(countries.country_id))
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
            ViewData["region_id"] = new SelectList(_context.regions, "region_id", "region_name", countries.region_id);
            return View(countries);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var countries = await _context.countries
                .Include(c => c.Regions)
                .FirstOrDefaultAsync(m => m.country_id == id);
            if (countries == null)
            {
                return NotFound();
            }

            return View(countries);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var countries = await _context.countries.FindAsync(id);
            if (countries != null)
            {
                _context.countries.Remove(countries);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountriesExists(string id)
        {
            return _context.countries.Any(e => e.country_id == id);
        }
    }
}
