using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LB1.data;
using LB1.Models;
using PagedList;

namespace LB1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly MVCMobileContext _context;

        public OrdersController(MVCMobileContext context)
        {
            _context = context;
        }


        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder,string SearchString,string CurrentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "client" : "";
            ViewBag.AddressSortParm = sortOrder == "address" ? "address_desc" : "address";
            ViewBag.ContactSortParm = sortOrder == "contact" ? "contact_desc" : "contact";
            ViewBag.PhoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            var orders = from s in _context.Orders.Include(o => o.Phone)
                         select s;

            if (!String.IsNullOrEmpty(SearchString))
                orders = orders.Where(s => s.Client.Contains(SearchString));
            if (SearchString != null)
                page = 1;
            else
                CurrentFilter = SearchString;
            ViewBag.CurrentFilter = SearchString;
            switch (sortOrder)
            {
                case "client":
                    orders = orders.OrderByDescending(s => s.Contact);
                    break;
                case "address":
                    orders = orders.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    orders = orders.OrderByDescending(s => s.Address);
                    break;
                case "contact":
                    orders = orders.OrderBy(s => s.Contact);
                    break;
                case "contact_desc":
                    orders = orders.OrderByDescending(s => s.Contact);
                    break;
                case "phone":
                    orders = orders.OrderBy(s => s.Phone);
                    break;
                case "phone_desc":
                    orders = orders.OrderByDescending(s => s.Phone);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Address);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/CreateNew
        public IActionResult CreateNew()
        {
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNew([Bind("Client,Address,Contact,PhoneId")] Order order)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "Id", order.PhoneId);
            return View(order);
        }

        //GET: Orders/Create/5
        public async Task<IActionResult> Create(int? id)
        {
            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            //ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "Name", phone.Id);
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Client,Address,Contact")] Order order)
        {
            order.PhoneId = id;
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "Id", order.PhoneId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "Id", order.PhoneId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Client,Address,Contact,PhoneId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            ViewData["PhoneId"] = new SelectList(_context.Phones, "Id", "Id", order.PhoneId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Phone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'MVCMobileContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return _context.Orders.Any(e => e.Id == id);
        }
    }
}
