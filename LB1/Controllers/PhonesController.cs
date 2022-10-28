using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LB1.Models;
using LB1.data;
namespace LB1.Controllers
{
    public class PhonesController : Controller
    {
        private readonly MVCMobileContext _context;

        public PhonesController(MVCMobileContext context)
        {
            _context = context;
        }

        // GET: Phones
        public async Task<IActionResult> Index(int PageNumber = 1)
        {
            int totalSize = _context.Phones.Count();
            int pageSize = 3;
            var mVCMobileContext = _context.Phones.Include(p => p.IdPhotoNavigation);
            ViewBag.PageNumber = PageNumber;
            ViewBag.PageCount = totalSize/pageSize;

            return View(await mVCMobileContext.Skip((PageNumber - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        // GET: Phones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.IdPhotoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        // GET: Phones/Create
        public IActionResult Create()
        {
            ViewData["IdPhoto"] = new SelectList(_context.Images, "Id", "Path");
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Company,Price,IdPhoto")] Phone phone)
        {
                _context.Add(phone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            ViewData["IdPhoto"] = new SelectList(_context.Images, "Id", "Id", phone.IdPhoto);
            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            ViewData["IdPhoto"] = new SelectList(_context.Images, "Id", "Path", phone.IdPhoto);
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Company,Price,IdPhoto")] Phone phone)
        {
            if (id != phone.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(phone);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(phone.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["IdPhoto"] = new SelectList(_context.Images, "Id", "Path", phone.IdPhoto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Phones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phones == null)
            {
                return NotFound();
            }

            var phone = await _context.Phones
                .Include(p => p.IdPhotoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phone == null)
            {
                return NotFound();
            }

            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phones == null)
            {
                return Problem("Entity set 'MVCMobileContext.Phones'  is null.");
            }
            var phone = await _context.Phones.FindAsync(id);
            if (phone != null)
            {
                _context.Phones.Remove(phone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneExists(int id)
        {
          return _context.Phones.Any(e => e.Id == id);
        }
    }
}
