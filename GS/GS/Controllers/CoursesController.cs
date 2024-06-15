using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GS.Models;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using GS.Momo;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace GS.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DACSDbContext _context;
        private readonly ClassesController _classesController;
        private readonly SubjectsController _subjectsController;
        private readonly UserManager<ApplicationUser> _userManager;

        private const string MomoEndpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
        private const string PartnerCode = "MOMO5RGX20191128";
        private const string AccessKey = "M8brj9K6E22vXoDB";
        private const string SecretKey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
        private const string ReturnUrl = "https://localhost:44336/MyCourses";
        private const string NotifyUrl = "https://localhost:44336/MyCourses";

        public CoursesController(UserManager<ApplicationUser> userManager, DACSDbContext context, ClassesController classesController, SubjectsController subjectsController)
        {
            _context = context;
            _classesController = classesController;
            _subjectsController = subjectsController;
            _userManager = userManager;
        }
		[HttpGet]
		public async Task<IActionResult> Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
			{
				return Json(new List<Course>());
			}

			var courses = await _context.Courses
										.Include(c => c.Class)
										.Include(c => c.Subject)
										.Include(c => c.ApplicationUser)
										.Where(c => c.NameCourse.Contains(searchString) ||
													c.Courseinformation.Contains(searchString) ||
													c.Class.Name.Contains(searchString) ||
													c.Subject.Namest.Contains(searchString) ||
													c.ApplicationUser.FullName.Contains(searchString))
										.Select(c => new
										{
											c.Idce,
											c.NameCourse,
											c.CourseImg,
											ClassName = c.Class.Name,
											AdvisorName = c.ApplicationUser.FullName,
											SubjectName = c.Subject.Namest,
											c.Price
										})
										.ToListAsync();

			return Json(courses);
		}
	
	[HttpGet]
		public async Task<IActionResult> GetCoursesByCategories(string categories)
		{
            
			var selectedCategories = categories?.Split(',') ?? new string[0];
			var courses = await _context.Courses //tìm theo lớp và môn
										.Include(c => c.Class)
                                        .Include(c=> c.Subject)
                                        .Include(c=>c.ApplicationUser)
										.Where(c => selectedCategories.Contains(c.Subject.Namest))
										.Where(c => selectedCategories.Contains(c.Class.Name))
										.ToListAsync();
			if (courses.IsNullOrEmpty() && categories == null)// nếu ds null thì tìm lấy tất cả course trong data 
			{
				courses = await _context.Courses
										.Include(c => c.Class)
										.Include(c => c.Subject)
										.Include(c => c.ApplicationUser)
										.ToListAsync();
			}
			if (courses.IsNullOrEmpty())// nếu ds null thì tìm lấy ds course của tất cả lớp theo môn 
            {
                courses= await _context.Courses
										.Include(c => c.Class)
										.Include(c => c.Subject)
										.Include(c => c.ApplicationUser)
										.Where(c => selectedCategories.Contains(c.Subject.Namest))
										.ToListAsync();
			}
			if (courses.IsNullOrEmpty())// nếu ds null thì tìm lấy ds course của tất cả môn theo lớp
			{
				courses = await _context.Courses
										.Include(c => c.Class)
										.Include(c => c.Subject)
										.Include(c => c.ApplicationUser)
										.Where(c => selectedCategories.Contains(c.Class.Name))
										.ToListAsync();
			}
			
			//var html = new StringBuilder();
			//html.Append("<table class='table'><thead><tr>");
			//html.Append("<th>Tên Khóa Học</th>");
			//html.Append("<th>Ngày Bắt Đầu</th>");
			//html.Append("<th>Ngày Kết Thúc</th>");
			//html.Append("<th>Thông Tin Khóa Học</th>");
			//html.Append("<th>Ngày Học</th>");
			//html.Append("<th>Ảnh Minh Họa</th>");
			//html.Append("<th>Giá</th>");
			//html.Append("<th>Lớp Học</th>");
			//html.Append("</tr></thead><tbody>");

			//foreach (var course in courses)
			//{

			//             html.Append("<tr>");
			//             html.AppendFormat("<td>{0}</td>", course.NameCourse);
			//             html.AppendFormat("<td>{0}</td>", course.Starttime?.ToString("dd/MM/yyyy"));
			//             html.AppendFormat("<td>{0}</td>", course.Endtime?.ToString("dd/MM/yyyy"));
			//             html.AppendFormat("<td>{0}</td>", course.Courseinformation);
			//             html.AppendFormat("<td>{0}</td>", course.DayInWeek);
			//             html.AppendFormat("<td>{0}</td>", !string.IsNullOrEmpty(course.CourseImg) ? $"<img src='{course.CourseImg}' alt='Course Image' style='max-width: 100px;' />" : "");
			//             html.AppendFormat("<td>{0}</td>", course.Price);
			//             html.AppendFormat("<td>{0}</td>", course.Class.Name);
			//             html.Append("</tr>");

			//         }
			//html.Append("</tbody></table>");


			//return Content(html.ToString(), "text/html");
			return PartialView("_View",courses);
			
		}

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
			foreach (var category1 in classes)
			{
				// Đếm số lượng sách của từng thể loại
				var bookCount1 = await CountBooksByClassAsync(category1.Idcs);
				bookCountByClass[category1.Name] = bookCount1;
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

            

            var suggestedCourses = _context.Courses
                .Where(c => c.Idcs == course.Idcs && c.Idce != course.Idce)
                .Include(c => c.ApplicationUser)
                .ToList();

            var viewModel = new CourseDetailsViewModel
            {
                Course = course,
                SuggestedCourses = suggestedCourses
            };

            return View(viewModel);
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
			return await _context.Courses.CountAsync(b => b.Idcs == categoryId);
		}
		public async Task<int> CountBooksBySubjectAsync(int categoryId)
		{
			// Sử dụng LINQ để đếm số lượng sách có categoryId tương ứng
			return await _context.Courses.CountAsync(b => b.Idst == categoryId);
		}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Pay(int courseId)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    var course = await _context.Courses.FirstOrDefaultAsync(c => c.Idce == courseId);

        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    var bill = new Bill
        //    {
        //        Name = course.NameCourse,
        //        DateOfPayment = DateTime.Now,
        //        TotalDiscount = 0,
        //        TotalMoney = course.Price,
        //        UserId = userId
        //    };

        //    _context.Bills.Add(bill);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Pay(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var orderId = Guid.NewGuid().ToString();
            var requestId = Guid.NewGuid().ToString();

            var rawHash = "partnerCode=" + PartnerCode +
                          "&accessKey=" + AccessKey +
                          "&requestId=" + requestId +
                          "&amount=" + course.Price +
                          "&orderId=" + orderId +
                          "&orderInfo=" + course.NameCourse +
                          "&returnUrl=" + ReturnUrl +
                          "&notifyUrl=" + NotifyUrl +
                          "&extraData=";

            MoMoSecurity crypto = new MoMoSecurity();
            string signature = crypto.signSHA256(rawHash, SecretKey);

            JObject message = new JObject
            {
                { "partnerCode", PartnerCode },
                { "accessKey", AccessKey },
                { "requestId", requestId },
                { "amount", course.Price.ToString() },
                { "orderId", orderId },
                { "orderInfo", course.NameCourse },
                { "returnUrl", ReturnUrl },
                { "notifyUrl", NotifyUrl },
                { "extraData", "" },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }
            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(MomoEndpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            if (jmessage.ContainsKey("payUrl"))
            {
                return Redirect(jmessage.GetValue("payUrl").ToString());
            }
            else
            {
                return BadRequest("Payment initiation failed.");
            }
            return View(course);
        }

        public async Task<IActionResult> PaymentResult(string partnerCode, string orderId, string requestId, string amount, string orderInfo, string orderType, string transId, string resultCode, string message, string payType, string responseTime, string extraData, string signature)
        {
            if (resultCode == "0")
            {
                var userId = _userManager.GetUserId(User);

                var bill = new Bill
                {
                    Name = orderInfo,
                    DateOfPayment = DateTime.Now,
                    TotalDiscount = 0,
                    TotalMoney = float.Parse(amount),
                    UserId = userId,
                    ApplicationUser = await _userManager.FindByIdAsync(userId)
                };

                _context.Bills.Add(bill);
                await _context.SaveChangesAsync();

                return View(Index());
            }
            else
            {
                ViewBag.ErrorMessage = message;
                return View("PaymentError");
            }
        }

        // Additional methods for handling payment notifications
        [HttpPost]
        public IActionResult PaymentNotify()
        {
            // Implement logic to handle payment notifications here
            return Ok();
        }
     
    }
}
