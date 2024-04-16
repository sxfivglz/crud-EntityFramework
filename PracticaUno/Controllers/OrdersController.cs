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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        //public async Task<IActionResult> Index()
        //{
           
        //    var applicationDbContext = _context.orders.Include(o => o.Customers).Include(x => x.Employees);
        //    return View(await applicationDbContext.ToListAsync());
        //}
        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10;
            var orders = _context.orders.Include(o => o.Customers).Include(x => x.Employees).AsQueryable();
            return View(await Pagination<Orders>.CreateAsync(orders, pageNumber ?? 1, pageSize));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .Include(o => o.Employees)
                .Include(o => o.Customers)
                .FirstOrDefaultAsync(m => m.order_id == id);

            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "Name");
            ViewData["salesman_id"] = new SelectList(_context.employees, "employee_id", "first_name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("order_id,order_date,status,customer_id,salesman_id")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["salesman_id"] = new SelectList(_context.employees, "employee_id", "first_name", orders.salesman_id);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .Include(o => o.Employees)
                .FirstOrDefaultAsync(m => m.order_id == id);

            if (orders == null)
            {
                return NotFound();
            }

            // Obtener la lista de clientes disponibles
            ViewData["customer_id"] = new SelectList(_context.customers, "customer_id", "Name", orders.customer_id);
            ViewData["salesman_id"] = new SelectList(_context.employees, "employee_id", "first_name", orders.salesman_id);

            return View(orders);
        }



        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("order_id,order_date,status,customer_id,salesman_id")] Orders orders)
        {
            if (id != orders.order_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.order_id))
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
            ViewData["salesman_id"] = new SelectList(_context.employees, "employee_id", "first_name", orders.salesman_id);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.orders
                .Include(o => o.Employees)
                .Include(o => o.Customers)
                .FirstOrDefaultAsync(m => m.order_id == id);

            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }
        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.orders.FindAsync(id);
            if (orders != null)
            {
                _context.orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.orders.Any(e => e.order_id == id);
        }
    }
}
