using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GS.Models;
using Microsoft.AspNetCore.Identity;

namespace GS.Controllers
{
    public class MyCoursesController : Controller
    {
        private readonly DACSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public MyCoursesController(DACSDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: MyCourses
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var coursesStudents = await _context.CoursesStudents
                .Include(cs => cs.Course)
                .ThenInclude(c => c.ApplicationUser) // Ensure the ApplicationUser is included
                .Where(cs => cs.UserId == userId)
                .ToListAsync();

            return View(coursesStudents);
        }

        // GET: MyCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var courseStudent = _context.CoursesStudents
                .Include(cs => cs.ApplicationUser)
                .Include(cs => cs.Course)
                .FirstOrDefault(cs => cs.Idcourses == id);

            if (courseStudent == null)
            {
                return NotFound();
            }

            var homeworkList = _context.HomeWork
                .Where(hw => hw.Idce == courseStudent.Course.Idce)
                .ToList();

            var homeworkSubmissions = _context.HomeWorkStudents
                .Include(hws => hws.ApplicationUser)
                .Include(hws => hws.HomeWork)
                .Where(hws => homeworkList.Select(hw => hw.Idhk).Contains(hws.Idhk))
                .ToList();

            ViewBag.HomeworkList = homeworkList;
            ViewBag.HomeworkSubmissions = homeworkSubmissions;

            return View(courseStudent);
        }

        // GET: MyCourses/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce");
            return View();
        }

        // POST: MyCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcourses,UserId,Idce")] CoursesStudent coursesStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursesStudent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", coursesStudent.UserId);
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce", coursesStudent.Idce);
            return View(coursesStudent);
        }

        // GET: MyCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesStudent = await _context.CoursesStudents.FindAsync(id);
            if (coursesStudent == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", coursesStudent.UserId);
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce", coursesStudent.Idce);
            return View(coursesStudent);
        }

        // POST: MyCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcourses,UserId,Idce")] CoursesStudent coursesStudent)
        {
            if (id != coursesStudent.Idcourses)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursesStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursesStudentExists(coursesStudent.Idcourses))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", coursesStudent.UserId);
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce", coursesStudent.Idce);
            return View(coursesStudent);
        }

        // GET: MyCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursesStudent = await _context.CoursesStudents
                .Include(c => c.ApplicationUser)
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Idcourses == id);
            if (coursesStudent == null)
            {
                return NotFound();
            }

            return View(coursesStudent);
        }

        // POST: MyCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coursesStudent = await _context.CoursesStudents.FindAsync(id);
            if (coursesStudent != null)
            {
                _context.CoursesStudents.Remove(coursesStudent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoursesStudentExists(int id)
        {
            return _context.CoursesStudents.Any(e => e.Idcourses == id);
        }
    }
}
