using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GS.Controllers
{
	public class PaymentModel : Controller
	{
        [Authorize]
        public IActionResult Index()
		{
			return View();
		}
	}
}
