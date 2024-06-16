using GS.Models;
using GS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Models;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.InkML;
namespace GS.Controllers
{
    public class TutorsController : Controller

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DACSDbContext _context;

        public TutorsController(UserManager<ApplicationUser> userManager, DACSDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> All()
        {
            return View();
        }
        // GET: ApplicationUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: ApplicationUser/Details/5
        public async Task<IActionResult> Details(/*string id*/)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var user = _context.Users
            //    .Include(u => u.Subjects)
            //    .Include(u => u.Classes)
            //    .Include(u => u.service)
            //    .FirstOrDefault(m => m.Id == id);

            //if (user == null)
            //{
            //    return NotFound();
            //}

            //var courses = _context.Courses
            //    .Where(c => c.ApplicationUser.Id == id)
            //    .ToList();

            //var viewModel = new ApplicationUserDetailsViewModel
            //{
            //    ApplicationUser = user,
            //    Courses = courses
            //};

            return View();
        }

        // GET: ApplicationUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUser/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber,Address,Age,Sex,CreditCardNumber,IDCard,IDCardImg,UserName,Email,PasswordHash")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(user);
        }

        // GET: ApplicationUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: ApplicationUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FullName,PhoneNumber,Address,Age,Sex,CreditCardNumber,IDCard,IDCardImg,UserName,Email,PasswordHash")] ApplicationUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: ApplicationUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
