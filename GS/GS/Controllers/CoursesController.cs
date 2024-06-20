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
using System.Security.Claims;

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
        public async Task<IActionResult> SearchCourse(string search, int page=1,int pageSize = 6)
        {
			IQueryable<Course> query = _context.Courses.Include(c => c.Class)
													  .Include(c => c.Subject)
													  .Include(c => c.ApplicationUser);
			// Kiểm tra nếu không có phân loại
			if (string.IsNullOrEmpty(search))
			{
				// Trả về tất cả khóa học nếu không có phân loại
				var courses = await query.ToListAsync();
				if (courses.IsNullOrEmpty())
				{
					return Content("Không Tìm Thấy Khóa Học Cần Tìm");
				}

				var totalItems = courses.Count;
				var coursesPaged = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
				var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
				var model = new PaginatedList<Course>(coursesPaged, totalItems, page, pageSize);

				return PartialView("_View", model);
			}
            else
            {
				var queryByClassAndSubject = _context.Courses.Include(c => c.Class)
															.Include(c => c.Subject)
															.Include(c => c.ApplicationUser)
															.Where(c => search.Contains(c.Class.Name) || search.Contains(c.Subject.Namest) || search.Contains(c.Subject.Namest))
															.ToList();
                if (queryByClassAndSubject.Count >0)
                {
					var totalItems = queryByClassAndSubject.Count;
					var coursesPaged = queryByClassAndSubject.Skip((page - 1) * pageSize).Take(pageSize).ToList();
					var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
					var model = new PaginatedList<Course>(coursesPaged, totalItems, page, pageSize);

					return PartialView("_View", model);
				}
				return Content("Không Tìm Thấy Khóa Học Cần Tìm");
			}
		}

		[HttpGet]
        public async Task<IActionResult> GetCoursesByCategories(string categories, int page = 1, int pageSize = 6)
        {
            var selectedCategories = categories?.Split(',') ?? new string[0];
            IQueryable<Course> query = _context.Courses.Include(c => c.Class)
                                                       .Include(c => c.Subject)
                                                       .Include(c => c.ApplicationUser);

            // Kiểm tra nếu không có phân loại
            if (string.IsNullOrEmpty(categories))
            {
                // Trả về tất cả khóa học nếu không có phân loại
                var courses = await query.ToListAsync();
                if (courses.IsNullOrEmpty())
                {
                    return Content("Không Tìm Thấy Khóa Học Cần Tìm");
                }

                var totalItems = courses.Count;
                var coursesPaged = courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                var model = new PaginatedList<Course>(coursesPaged, totalItems, page, pageSize);

                return PartialView("_View", model);
            }
            else
            {
                // Query theo cả lớp và môn
                var queryByClassAndSubject = _context.Courses.Include(c => c.Class)
                                                            .Include(c => c.Subject)
                                                            .Include(c => c.ApplicationUser)
                                                            .Where(c => selectedCategories.Contains(c.Class.Name) && selectedCategories.Contains(c.Subject.Namest))
                                                            .ToList();

                if (!queryByClassAndSubject.IsNullOrEmpty())
                {
                    // Phân trang - thực hiện lọc với trang hiện tại và số lượng trên trang
                    var totalItems = queryByClassAndSubject.Count;
                    var coursesPaged = queryByClassAndSubject.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                    var model = new PaginatedList<Course>(coursesPaged, totalItems, page, pageSize);

                    return PartialView("_View", model);
                }

                // Query theo lớp
                var queryByClass = _context.Courses.Include(c => c.Class)
                                                   .Include(c => c.Subject)
                                                   .Include(c => c.ApplicationUser)
                                                   .Where(c => selectedCategories.Contains(c.Class.Name))
                                                   .ToList();

                if (!queryByClass.IsNullOrEmpty())
                {
                    // Phân trang - thực hiện lọc với trang hiện tại và số lượng trên trang
                    var totalItems = queryByClass.Count;
                    var coursesPaged = queryByClass.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                    var model = new PaginatedList<Course>(coursesPaged, totalItems, page, pageSize);

                    return PartialView("_View", model);
                }

                // Query theo môn
                var queryBySubject = _context.Courses.Include(c => c.Class)
                                                     .Include(c => c.Subject)
                                                     .Include(c => c.ApplicationUser)
                                                     .Where(c => selectedCategories.Contains(c.Subject.Namest))
                                                     .ToList();

                if (!queryBySubject.IsNullOrEmpty())
                {
                    // Phân trang - thực hiện lọc với trang hiện tại và số lượng trên trang
                    var totalItems = queryBySubject.Count;
                    var coursesPaged = queryBySubject.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
                    var model = new PaginatedList<Course>(coursesPaged, totalItems, page, pageSize);

                    return PartialView("_View", model);
                }

                // Nếu không tìm thấy khóa học nào phù hợp với tiêu chí lọc, trả về thông báo "Không Tìm Thấy Khóa Học Cần Tìm"
                return Content("Không Tìm Thấy Khóa Học Cần Tìm");
            }
        }





		[Authorize]

		public async Task<IActionResult> Index()
        {
            // Assuming you have a way to get the currently logged-in user's ID
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Query the database to fetch the courses for the logged-in user
            var userCoursesQuery = _context.Courses
                .Include(c => c.ApplicationUser)
                .Include(c => c.Class)
                .Include(c => c.Subject)
                .Include(c => c.TimeCourse)
                .Where(c => c.UserId == userId);  // Filter by the logged-in user ID

            // Execute the query and get the list of courses
            var userCourses = await userCoursesQuery.ToListAsync();

            // Return the view with the filtered list of courses
            return View(userCourses);
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
            if(suggestedCourses==null)
            {
				suggestedCourses = _context.Courses
				.Where(c => c.Idst == course.Idst && c.Idst != course.Idst)
				.Include(c => c.ApplicationUser)
				.ToList();
			}
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.ApplicationUsers.SingleOrDefault(u => u.Id == userId);
            var classes = _context.Class.ToList();
            var subjects = _context.Subjects.ToList();
            var timeCourses = _context.TimeCourses.ToList();

            if (user == null || classes == null || subjects == null || timeCourses == null)
            {
                // Xử lý lỗi nếu dữ liệu không có sẵn
                return View("Error", new ErrorViewModel { ErrorMessage = "Dữ liệu không tìm thấy." });
            }

            ViewData["UserId"] = new SelectList(new List<ApplicationUser> { user }, "Id", "FullName", user.Id);
            ViewData["Idcs"] = new SelectList(classes, "Idcs", "Name");
            ViewData["Idst"] = new SelectList(subjects, "Idst", "Namest");
            ViewData["Idtimece"] = new SelectList(timeCourses, "Idtimece", "Idtimece");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course, IFormFile courseImg)
        {
            if (ModelState.IsValid)
            {
                if (course.Starttime >= DateTime.Now)
                {
                    if (course.Starttime < course.Endtime)
                    {
                        if (courseImg != null)
                        {
                            course.CourseImg = await SaveImage(courseImg);
                        }

                        _context.Courses.Add(course);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ngày giờ bắt đầu của khóa học phải lớn hơn ngày kết thúc khóa học!.");
                    }    
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ngày giờ bắt đầu của khóa học phải lớn hơn hoặc bằng thời gian hiện tại.");
                }
            }

            // Nếu model state không hợp lệ, thiết lập lại ViewData
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.ApplicationUsers.SingleOrDefault(u => u.Id == userId);
            var classes = _context.Class.ToList();
            var subjects = _context.Subjects.ToList();
            var timeCourses = _context.TimeCourses.ToList();

            if (user == null || classes == null || subjects == null || timeCourses == null)
            {
                // Xử lý lỗi nếu dữ liệu không có sẵn
                return View("Error", new ErrorViewModel { ErrorMessage = "Dữ liệu không tìm thấy." });
            }

            ViewData["UserId"] = new SelectList(new List<ApplicationUser> { user }, "Id", "FullName", user.Id);
            ViewData["Idcs"] = new SelectList(classes, "Idcs", "Name", course.Idcs);
            ViewData["Idst"] = new SelectList(subjects, "Idst", "Namest", course.Idst);
            ViewData["Idtimece"] = new SelectList(timeCourses, "Idtimece", "Idtimece", course.Idtimece);

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
        public async Task<IActionResult> Edit(int id,  Course course,IFormFile CourseImg)
        {
            if (id != course.Idce)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (CourseImg != null)
                    {
                        course.CourseImg = await SaveImage(CourseImg);
                    }
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

            var bill = new Bill
            {
                Name = "Course",
                DateOfPayment = DateTime.Now,
                TotalDiscount = 0,
                TotalMoney = course.Price,
                UserId = userId,
                ApplicationUser = await _userManager.FindByIdAsync(userId)
            };

            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();

            // Get the course ID from the orderInfo or a way to associate it correctly

            if (course != null)
            {
                var courseStudent = new CoursesStudent
                {
                    UserId = userId,
                    Idce = course.Idce
                };

                _context.CoursesStudents.Add(courseStudent);
                await _context.SaveChangesAsync();
            }

            if (jmessage.ContainsKey("payUrl"))
            {
                return Redirect(jmessage.GetValue("payUrl").ToString());
            }
            else
            {
                return BadRequest("Payment initiation failed.");
            }
            // return statement after the redirection is not needed
        }

        public async Task<IActionResult> PaymentResult()
        {
            var query = Request.Query;

            string partnerCode = query["partnerCode"];
            string orderId = query["orderId"];
            string requestId = query["requestId"];
            string amount = query["amount"];
            string orderInfo = query["orderInfo"];
            string orderType = query["orderType"];
            string transId = query["transId"];
            string resultCode = query["resultCode"];
            string message = query["message"];
            string payType = query["payType"];
            string responseTime = query["responseTime"];
            string extraData = query["extraData"];
            string signature = query["signature"];

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

                // Get the course ID from the orderInfo or a way to associate it correctly
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.NameCourse == orderInfo);

                if (course != null)
                {
                    var courseStudent = new CoursesStudent
                    {
                        UserId = userId,
                        Idce = course.Idce
                    };

                    _context.CoursesStudents.Add(courseStudent);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Details2(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Class)
                .Include(c => c.TimeCourse)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Idce == id);

            if (course == null)
            {
                return NotFound();
            }

            // Sort homework assignments and get top 70% based on some criteria.
            
           
            var homeworkList = _context.HomeWork
        .Where(hw => hw.Idce == course.Idce)
        .ToList();
            int homeworkCount = homeworkList.Count;
            int displayCount = (int)(homeworkCount * 0.7);

            var filteredHomeworks = homeworkList.ToList();

            var viewModel = new CourseDetailsViewModelWithHomeWork
            {
                Course = course,
                HomeworkList = filteredHomeworks
            };

            return View(viewModel);
        }

    }
}
