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
    public class CoursesController : Controller
    {
        private readonly DACSDbContext _context;
        private readonly ClassesController _classesController;
        private readonly SubjectsController _subjectsController;

        public CoursesController(DACSDbContext context, ClassesController classesController, SubjectsController subjectsController)
        {
            _context = context;
            _classesController = classesController;
            _subjectsController = subjectsController;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var dACSDbContext = _context.Courses.Include(c => c.ApplicationUser).Include(c => c.Class).Include(c => c.Subject).Include(c => c.TimeCourse);
            return View(await dACSDbContext.ToListAsync());
        }
		public async Task<IEnumerable<Course>> GetAllAsync()
		{
			// return await _context.Products.ToListAsync();
			return await _context.Courses
			.Include(p => p.Class).Include(p=>p.Subject).Include(p=>p.ApplicationUser) // Include thông tin về category
			.ToListAsync();
		}
		public async Task<IActionResult> All(string searchString, string categoryName)
        {
			var class1 = _context.Class.ToList();
            var course1 = _context.Courses.ToList();
			var course = await GetAllAsync();
            var classes = await _classesController.GetAllAsync();
            var subjects = await _subjectsController.GetAllAsync();

			if (!string.IsNullOrEmpty(searchString))
			{
				course = course.Where(p => p.NameCourse.ToLower().Contains(searchString.ToLower()));

			}
			var bookCountByClass = new Dictionary<string, int>();
			if (!string.IsNullOrEmpty(categoryName))
			{
				course = course.Where(p => p.Class.Name == categoryName);
			}
			foreach (var category in classes)
			{
				// Đếm số lượng sách của từng thể loại
				var bookCount1 = await CountBooksByClassAsync(category.Idcs);
				bookCountByClass[category.Name] = bookCount1;
			}
			var bookCountBySubjects = new Dictionary<string, int>();
			if (!string.IsNullOrEmpty(categoryName))
			{
				course = course.Where(p => p.Class.Name == categoryName);
			}
			foreach (var category in subjects)
			{
				// Đếm số lượng sách của từng thể loại
				var bookCount2 = await CountBooksBySubjectAsync(category.Idst);
				bookCountBySubjects[category.Namest] = bookCount2;
			}

			ViewBag.BookCountBySubject = bookCountBySubjects;
			ViewBag.BookCountByClass = bookCountByClass;
			return View(course);
            
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["Idcs"] = new SelectList(_context.Class, "Idcs", "Name");
            ViewData["Idst"] = new SelectList(_context.Subjects, "Idst", "Idst");
            ViewData["Idtimece"] = new SelectList(_context.TimeCourses, "Idtimece", "Idtimece");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create( Course course, IFormFile courseImg)
        
        {

            if (ModelState.IsValid)
            {
                if (courseImg != null)
                {
                    course.CourseImg = await SaveImage(courseImg);
                }
                await _context.AddAsync(course);
                 _context.Courses.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", course.UserId);
            ViewData["Idcs"] = new SelectList(_context.Class, "Idcs", "Name", course.Idcs);
            ViewData["Idst"] = new SelectList(_context.Subjects, "Idst", "Idst", course.Idst);
            ViewData["Idtimece"] = new SelectList(_context.TimeCourses, "Idtimece", "Idtimece", course.Idtimece);
            return View(course);
        }
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

        // GET: Courses/Edit/5
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

        // POST: Courses/Edit/5
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

        // GET: Courses/Delete/5
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

        // POST: Courses/Delete/5
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
		public async Task<int> CountBooksByClassAsync(int categoryId)
		{
			// Sử dụng LINQ để đếm số lượng sách có categoryId tương ứng
			return await _context.Class.CountAsync(b => b.Idcs == categoryId);
		}
		public async Task<int> CountBooksBySubjectAsync(int categoryId)
		{
			// Sử dụng LINQ để đếm số lượng sách có categoryId tương ứng
			return await _context.Subjects.CountAsync(b => b.Idst == categoryId);
		}
	}
}
