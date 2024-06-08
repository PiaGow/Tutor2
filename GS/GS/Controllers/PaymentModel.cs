using Microsoft.AspNetCore.Mvc;

namespace GS.Controllers
{
	public class PaymentModel : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
