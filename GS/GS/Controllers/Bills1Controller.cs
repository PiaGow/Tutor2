using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GS.Controllers
{
    public class Bills1Controller : Controller
    {
        private readonly DACSDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Bills1Controller(DACSDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bills1
        //public async Task<IActionResult> Index()
        //{
        //    var dACSDbContext = _context.Bills.Include(b => b.ApplicationUser);
        //    return View(await dACSDbContext.ToListAsync());
        //}

        // GET: Bills1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.IdBill == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bills1/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Bills1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBill,Name,DateOfPayment,TotalDiscount,TotalMoney,UserId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bill.UserId);
            return View(bill);
        }

        // GET: Bills1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bill.UserId);
            return View(bill);
        }

        // POST: Bills1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBill,Name,DateOfPayment,TotalDiscount,TotalMoney,UserId")] Bill bill)
        {
            if (id != bill.IdBill)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.IdBill))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", bill.UserId);
            return View(bill);
        }

        // GET: Bills1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.IdBill == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bills1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.IdBill == id);
        }
       
    
        [Authorize]
      

        // POST: Bill/CreateBill
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBill(int courseId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            var course = await _context.Courses.FindAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            var bill = new Bill
            {
                Name = $"Thanh toán cho khóa học {course.NameCourse}",
                DateOfPayment = DateTime.Now,
                TotalDiscount = 0, // Bạn có thể tính toán giảm giá nếu cần
                TotalMoney = course.Price,
                UserId = user.Id,
                ApplicationUser = user
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Bill");
        }
            

        // GET: Bill/Index
        public async Task<IActionResult> Index()
        {
            var bills = await _context.Bills
                                        .Include(b => b.ApplicationUser)
                                        .ToListAsync();
            return View(bills);
        }
    }
}



