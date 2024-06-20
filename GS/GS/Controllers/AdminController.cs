// AdminController.cs

using Microsoft.AspNetCore.Mvc;
using GS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // Assuming your models are defined here

public class AdminController : Controller
{
    private readonly DACSDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;


    public AdminController(DACSDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

	// User Management
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult ManageUsers()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

	// Course Management
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult ManageCourses()
    {
        var courses = _context.Courses.ToList();
        return View(courses);
    }

	// Class Management
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult ManageClasses()
    {
        var classes = _context.Class.ToList();
        return View(classes);
    }

	// Subject Management
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult ManageSubjects()
    {
        var subjects = _context.Subjects.ToList();
        return View(subjects);
    }

	// Create User
	//public IActionResult CreateUser()
	//{
	//    return View();
	//}

	//[HttpPost]
	//public IActionResult CreateUser(User user)
	//{
	//    if (ModelState.IsValid)
	//    {
	//        _context.Users.Add(user);
	//        _context.SaveChanges();
	//        return RedirectToAction("ManageUsers");
	//    }
	//    return View(user);
	//}

	// Edit User
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult EditUser(string id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
	[Authorize(Roles = Role.Role_Admin)]

	public async Task<IActionResult> EditUser(ApplicationUser user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.Age = user.Age;
                existingUser.Sex = user.Sex;
                existingUser.CreditCardNumber = user.CreditCardNumber;
                existingUser.IDCard = user.IDCard;
                existingUser.IDCardImg = user.IDCardImg;
                existingUser.ValidUser = user.ValidUser;

                var result = await _userManager.UpdateAsync(existingUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("ManageUsers");
                }
            }
        }
        return View(user); // Return the same view with validation errors if update fails
    }

	// Delete User
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult DeleteUser(string id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return RedirectToAction("ManageUsers");
    }
	[Authorize(Roles = Role.Role_Admin)]
	public IActionResult CreateClass()
    {
        return View();
    }

    // POST: Classes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
	[Authorize(Roles = Role.Role_Admin)]
	public async Task<IActionResult> CreateClass([Bind("Idcs,Name")] Class @class)
    {
        if (ModelState.IsValid)
        {
            _context.Add(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction("ManageClasses"); // Redirect after successful creation
        }
        return View(@class); // Return view with validation errors
    }
	[Authorize(Roles = Role.Role_Admin)]
	public async Task<IActionResult> EditClass(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var @class = await _context.Class.FindAsync(id);
        if (@class == null)
        {
            return NotFound();
        }
        return View(@class);
    }

    // POST: Classes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
	[Authorize(Roles = Role.Role_Admin)]
	public async Task<IActionResult> EditClass(int id, [Bind("Idcs,Name")] Class @class)
    {
        if (id != @class.Idcs)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(@class);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(@class.Idcs))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("ManageClasses", "/Admin"); 
        }
        return View(ManageClasses);
    }

	// GET: Classes/Delete/5
	[Authorize(Roles = Role.Role_Admin)]
	public async Task<IActionResult> DeleteClass(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var @class = await _context.Class
            .FirstOrDefaultAsync(m => m.Idcs == id);
        if (@class == null)
        {
            return NotFound();
        }

        return View(@class);
    }

    // POST: Classes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
	[Authorize(Roles = Role.Role_Admin)]
	public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var @class = await _context.Class.FindAsync(id);
        if (@class != null)
        {
            _context.Class.Remove(@class);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("ManageClasses", "/Admin");
    }

    private bool ClassExists(int id)
    {
        return _context.Class.Any(e => e.Idcs == id);
    }
    // Similar CRUD methods would be implemented for courses, classes, and subjects.
}
