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
    public class HomeWorkController : Controller
    {
        private readonly DACSDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeWorkController(DACSDbContext context, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: HomeWork
        public async Task<IActionResult> Index()
        {
            var dACSDbContext = _context.HomeWork.Include(h => h.Course);
            return View(await dACSDbContext.ToListAsync());
        }

        // GET: HomeWork/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeWork = await _context.HomeWork
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.Idhk == id);
            if (homeWork == null)
            {
                return NotFound();
            }

            return View(homeWork);
        }

        // GET: HomeWork/Create
        public IActionResult Create(int id)
        {
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce");
            return View();
        }

        // POST: HomeWork/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idhk,Namehk,Timestart,Timeend,Details,Idce")] HomeWork homeWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce", homeWork.Idce);
            return View(homeWork);
        }

        // GET: HomeWork/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeWork = await _context.HomeWork.FindAsync(id);
            if (homeWork == null)
            {
                return NotFound();
            }
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce", homeWork.Idce);
            return View(homeWork);
        }

        // POST: HomeWork/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idhk,Namehk,Timestart,Timeend,Details,Idce")] HomeWork homeWork)
        {
            if (id != homeWork.Idhk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeWorkExists(homeWork.Idhk))
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
            ViewData["Idce"] = new SelectList(_context.Courses, "Idce", "Idce", homeWork.Idce);
            return View(homeWork);
        }

        // GET: HomeWork/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeWork = await _context.HomeWork
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.Idhk == id);
            if (homeWork == null)
            {
                return NotFound();
            }

            return View(homeWork);
        }

        // POST: HomeWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeWork = await _context.HomeWork.FindAsync(id);
            if (homeWork != null)
            {
                _context.HomeWork.Remove(homeWork);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeWorkExists(int id)
        {
            return _context.HomeWork.Any(e => e.Idhk == id);
        }
        public async Task<IActionResult> ViewHomework(int id)
        {
            var homework = await _context.HomeWork
                .Include(h => h.Course)
                .FirstOrDefaultAsync(m => m.Idhk == id);

            if (homework == null)
            {
                return NotFound();
            }

            var submissions = await _context.HomeWorkStudents
                .Include(h => h.ApplicationUser)
                .Where(hs => hs.Idhk == id)
                .ToListAsync();

            ViewBag.HomeworkSubmissions = submissions;
            return View(homework);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitHomework(int Idhk)
        {
            // Logic for submitting homework goes here
            // For now, just redirect back to the homework details page
            return RedirectToAction("ViewHomework", new { id = Idhk });
        }
    }
}
