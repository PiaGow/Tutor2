using GS.Models;
using GS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Models;
namespace GS.Controllers
{
	public class TutorsController : Controller
		
	{
        
		private DACSDbContext _dacsdbContext;
		public TutorsController(DACSDbContext applicationUser)
		{
			_dacsdbContext = applicationUser;
		}
  //      private readonly ITutorRepository _productRepository;
		//public TutorsController(ITutorRepository productRepository)

		//{
		//	_productRepository = productRepository;
		//}
		//[Authorize]
		public async Task<IActionResult> Index()
		{
			return View(_dacsdbContext.Users.ToList());
			
		}
		//[Area("Admin")]
		//[Authorize(Roles = Role.Role_Admin)]
		
		// Xử lý thêm sản phẩm mới
		//[HttpPost]


		//private async Task<string> SaveImage(IFormFile image)
		//{
		//	var savePath = Path.Combine("wwwroot/images", image.FileName);

		//	using (var fileStream = new FileStream(savePath, FileMode.Create))
		//	{
		//		await image.CopyToAsync(fileStream);
		//	}
		//	return "/images/" + image.FileName;
		//}
		//public async Task<IActionResult> Display(string id)
		//{
		//	var product = await _productRepository.GetByIdAsync(id);
		//	if (product == null)
		//	{
		//		return NotFound();
		//	}
		//	return View(product);
		//}
		//// Hiển thị form cập nhật sản phẩm
		////[Authorize(Roles = Role.Role_Admin)]
		//public async Task<IActionResult> Update(string id)
		//{
		//	var product = await _productRepository.GetByIdAsync(id);
		//	if (product == null)
		//	{
		//		return NotFound();
		//	}
			
		//	return View(product);
		//}

		//[HttpPost]
		//public async Task<IActionResult> Update(string id, ApplicationUser product, IFormFile imageUrl)
		//{
		//	ModelState.Remove("ImageUrl");

		//	if (id != product.Id)
		//	{
		//		return NotFound();
		//	}
		//	if (ModelState.IsValid)
		//	{
		//		var existingProduct = await

		//		_productRepository.GetByIdAsync(id);
		//		if (imageUrl == null)
		//		{
		//			product.IDCardImg = existingProduct.IDCardImg;
		//		}
		//		else
		//		{
		//			// Lưu hình ảnh mới
		//			product.IDCardImg = await SaveImage(imageUrl);
		//		}

		//		existingProduct.FullName = product.FullName;
		//		existingProduct.Address = product.Address;
		//		existingProduct.PhoneNumber = product.PhoneNumber;
		//		existingProduct.Age = product.Age;
		//		existingProduct.IDCardImg = product.IDCardImg;
		//		existingProduct.Class = existingProduct.Class;
		//		existingProduct.Subject = product.Subject;
		//		await _productRepository.UpdateAsync(existingProduct);
		//		return RedirectToAction(nameof(Index));
		//	}
		//	return View(product);
		//}

		////[Authorize(Roles = Role.Role_Admin)]
		//public async Task<IActionResult> Delete(string id)
		//{
		//	var product = await _productRepository.GetByIdAsync(id);
		//	if (product == null)
		//	{
		//		return NotFound();
		//	}
		//	return View(product);
		//}
		// Xử lý xóa sản phẩm
		//[HttpPost, ActionName("DeleteConfirmed")]
		//public async Task<IActionResult> DeleteConfirmed(int id)
		//{
		//	await _productRepository.DeleteAsync(id);
		//	return RedirectToAction(nameof(Index));
		//}
	}
}
