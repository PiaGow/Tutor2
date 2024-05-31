using GS.Models;
using GS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Models;
using Microsoft.EntityFrameworkCore;
namespace GS.Controllers
{
	public class TutorsController : Controller
		
	{
        private UserManager<ApplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;
		private DACSDbContext _dacsdbContext;
		public TutorsController(DACSDbContext applicationUser, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_dacsdbContext = applicationUser;
			_userManager = userManager;
			_roleManager = roleManager;

		}
		//      private readonly ITutorRepository _productRepository;
		//public TutorsController(ITutorRepository productRepository)

		//{
		//	_productRepository = productRepository;
		//}
		//[Authorize]
		public IActionResult All()
		{
			return View();
		}
		public async Task<IActionResult> Index()
		{
			//return View(_dacsdbContext.Users.ToList());
            var User = _userManager.Users;
            var UserinTutorRole = (from user in _dacsdbContext.Users
                                   join userRole in _dacsdbContext.UserRoles
                                   on user.Id equals userRole.UserId
                                   join role in _dacsdbContext.Roles
                                   on userRole.RoleId equals role.Id
                                   where role.Name == "Gia Sư"
                                   select user).ToList();
            return View(UserinTutorRole);

        }
		//public async Task<IActionResult> Detail()
		//{
		//	var User = _userManager.Users;
		//	var UserinTutorRole = (from user in _dacsdbContext.Users
		//								join userRole in _dacsdbContext.UserRoles
		//								on user.Id equals userRole.UserId
		//								join role in _dacsdbContext.Roles
		//								on userRole.RoleId equals role.Id
		//								where role.Name =="Gia Sư"
		//								select user).ToList();
		//	return View(UserinTutorRole);
		//},,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
        public async Task<IActionResult> Details(int? id)
        {
            var userID = _userManager.GetUserId(HttpContext.User);
            if(userID==null)
            {
                return RedirectToAction("Identity","Login", "Account");

            }
            else
            {
                ApplicationUser user = _userManager.FindByIdAsync(userID).Result;
                return View(user);
            }
        }

        // GET: Classes/Create
        
    }
}
