using GS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GS.Controllers
{
	public class TutorController : Controller
	{
		private readonly DACSDbContext _context;

		public TutorController(DACSDbContext context)
		{
			_context = context;
		}

		// GET: Courses
		public async Task<IActionResult> Index()
		{
			var dACSDbContext = _context.ApplicationUsers.Include(c => c.Id);
			return View(await dACSDbContext.ToListAsync());
		}

		// GET: Courses/Details/5
		//public async Task<IActionResult> Details(int? id)
		//{
			//if (id == null)
			//{
			//	return NotFound();
			//}

			//var applicationuser = await _context.ApplicationUsers
			//	.Include(c => c.Id)
			//	.FirstOrDefaultAsync(m => m.Id == id);
			//if (applicationuser == null)
			//{
			//	return NotFound();
			//}

			//return View(applicationuser);
		//}

		// GET: Courses/Create
		public IActionResult Create()
		{
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
			return View();
		}

		// POST: Courses/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Idce,NameCourse,Starttime,Endtime,Courseinformation,DayInWeek,UserId,ClassLink,Price,Idst,Idtimece,Idcs,Idhk")] Course course)
		{
			if (ModelState.IsValid)
			{
				_context.Add(course);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
			return View(course);
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
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
			return View(course);
		}

		// POST: Courses/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Idce,NameCourse,Starttime,Endtime,Courseinformation,DayInWeek,UserId,ClassLink,Price,Idst,Idtimece,Idcs,Idhk")] Course course)
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
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
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
	}
}

	

