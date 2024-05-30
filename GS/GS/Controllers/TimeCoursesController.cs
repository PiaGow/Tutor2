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
    public class TimeCoursesController : Controller
    {
        private readonly DACSDbContext _context;

        public TimeCoursesController(DACSDbContext context)
        {
            _context = context;
        }

        // GET: TimeCourses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TimeCourses.ToListAsync());
        }

        // GET: TimeCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeCourse = await _context.TimeCourses
                .FirstOrDefaultAsync(m => m.Idtimece == id);
            if (timeCourse == null)
            {
                return NotFound();
            }

            return View(timeCourse);
        }

        // GET: TimeCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idtimece,Timestart,Timeend")] TimeCourse timeCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeCourse);
        }

        // GET: TimeCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeCourse = await _context.TimeCourses.FindAsync(id);
            if (timeCourse == null)
            {
                return NotFound();
            }
            return View(timeCourse);
        }

        // POST: TimeCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idtimece,Timestart,Timeend")] TimeCourse timeCourse)
        {
            if (id != timeCourse.Idtimece)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeCourseExists(timeCourse.Idtimece))
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
            return View(timeCourse);
        }

        // GET: TimeCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeCourse = await _context.TimeCourses
                .FirstOrDefaultAsync(m => m.Idtimece == id);
            if (timeCourse == null)
            {
                return NotFound();
            }

            return View(timeCourse);
        }

        // POST: TimeCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeCourse = await _context.TimeCourses.FindAsync(id);
            if (timeCourse != null)
            {
                _context.TimeCourses.Remove(timeCourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeCourseExists(int id)
        {
            return _context.TimeCourses.Any(e => e.Idtimece == id);
        }
    }
}
