using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GS.Models;

namespace GS.Controllers
{
    public class RattingsController : Controller
    {
        private readonly DACSDbContext _context;

        public RattingsController(DACSDbContext context)
        {
            _context = context;
        }

        // GET: Rattings
        public async Task<IActionResult> Index()
        {
            var dACSDbContext = _context.Ratings.Include(r => r.ApplicationUser);
            return View(await dACSDbContext.ToListAsync());
        }

        // GET: Rattings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratting = await _context.Ratings
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.GradeNumber == id);
            if (ratting == null)
            {
                return NotFound();
            }

            return View(ratting);
        }

        // GET: Rattings/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Rattings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GradeNumber,CourseNumber,ReviewNumber,UserId")] Ratting ratting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ratting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ratting.UserId);
            return View(ratting);
        }

        // GET: Rattings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratting = await _context.Ratings.FindAsync(id);
            if (ratting == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ratting.UserId);
            return View(ratting);
        }

        // POST: Rattings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeNumber,CourseNumber,ReviewNumber,UserId")] Ratting ratting)
        {
            if (id != ratting.GradeNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ratting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RattingExists(ratting.GradeNumber))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ratting.UserId);
            return View(ratting);
        }

        // GET: Rattings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ratting = await _context.Ratings
                .Include(r => r.ApplicationUser)
                .FirstOrDefaultAsync(m => m.GradeNumber == id);
            if (ratting == null)
            {
                return NotFound();
            }

            return View(ratting);
        }

        // POST: Rattings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ratting = await _context.Ratings.FindAsync(id);
            if (ratting != null)
            {
                _context.Ratings.Remove(ratting);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RattingExists(int id)
        {
            return _context.Ratings.Any(e => e.GradeNumber == id);
        }
    }
}
