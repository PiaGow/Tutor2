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
    public class MyCoursesController : Controller
    {
        private readonly DACSDbContext _context;

        public MyCoursesController(DACSDbContext context)
        {
            _context = context;
        }

        // GET: MyCourses
        public async Task<IActionResult> Index()
        {
            var dACSDbContext = _context.Courses.Include(c => c.ApplicationUser).Include(c => c.Class).Include(c => c.Subject).Include(c => c.TimeCourse);
            return View(await dACSDbContext.ToListAsync());
        }

        // GET: MyCourses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.ApplicationUser)
                .Include(c => c.Class)
                .Include(c => c.Subject)
                
                .FirstOrDefaultAsync(m => m.Idce == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: MyCourses/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["Idcs"] = new SelectList(_context.Class, "Idcs", "Name");
            ViewData["Idst"] = new SelectList(_context.Subjects, "Idst", "Idst");
            ViewData["Idtimece"] = new SelectList(_context.TimeCourses, "Idtimece", "Idtimece");
            return View();
        }

        // POST: MyCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idce,NameCourse,Starttime,Endtime,Courseinformation,DayInWeek,CourseImg,UserId,ClassLink,Price,Idst,Idtimece,Idcs")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", course.UserId);
            ViewData["Idcs"] = new SelectList(_context.Class, "Idcs", "Name", course.Idcs);
            ViewData["Idst"] = new SelectList(_context.Subjects, "Idst", "Idst", course.Idst);
            ViewData["Idtimece"] = new SelectList(_context.TimeCourses, "Idtimece", "Idtimece", course.Idtimece);
            return View(course);
        }

        // GET: MyCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", course.UserId);
            ViewData["Idcs"] = new SelectList(_context.Class, "Idcs", "Name", course.Idcs);
            ViewData["Idst"] = new SelectList(_context.Subjects, "Idst", "Idst", course.Idst);
            ViewData["Idtimece"] = new SelectList(_context.TimeCourses, "Idtimece", "Idtimece", course.Idtimece);
            return View(course);
        }

        // POST: MyCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idce,NameCourse,Starttime,Endtime,Courseinformation,DayInWeek,CourseImg,UserId,ClassLink,Price,Idst,Idtimece,Idcs")] Course course)
        {
            if (id != course.Idce)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Idce))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", course.UserId);
            ViewData["Idcs"] = new SelectList(_context.Class, "Idcs", "Name", course.Idcs);
            ViewData["Idst"] = new SelectList(_context.Subjects, "Idst", "Idst", course.Idst);
            ViewData["Idtimece"] = new SelectList(_context.TimeCourses, "Idtimece", "Idtimece", course.Idtimece);
            return View(course);
        }

        // GET: MyCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.ApplicationUser)
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.TimeCourse)
                .FirstOrDefaultAsync(m => m.Idce == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: MyCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Idce == id);
        }
    }
}
