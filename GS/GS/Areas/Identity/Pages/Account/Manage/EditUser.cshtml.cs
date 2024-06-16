using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using GS.Models;

namespace GS.Areas.Identity.Pages.Account.Manage
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<EditUserModel> _logger;

        public EditUserModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<EditUserModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string FullName { get; set; }

            [Phone]
            public string PhoneNumber { get; set; }

            public string Address { get; set; }

            public string Age { get; set; }

            public string Sex { get; set; }

            public string CreditCardNumber { get; set; }

            public string IDCard { get; set; }

            [DataType(DataType.Upload)]
            [Display(Name = "ID Card Image")]
            public IFormFile IDCardImg { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Input = new InputModel
            {
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Age = user.Age,
                Sex = user.Sex,
                CreditCardNumber = user.CreditCardNumber,
                IDCard = user.IDCard,
                IDCardImg = null  // Or set the file path or base64 string of the image here if available
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            // File handling (upload the file to the server)
            if (Input.IDCardImg != null)
            {
                // Assuming you have a file storage mechanism to store files
                // Here you would upload the file and get the file path or URL
                // Example code to store file:
                // var filePath = Path.Combine("uploads", Input.IDCardImg.FileName);
                // using (var stream = new FileStream(filePath, FileMode.Create))
                // {
                //     await Input.IDCardImg.CopyToAsync(stream);
                // }
                // user.IDCardImg = filePath;

                // For demonstration purposes, let's set the file name directly
                user.IDCardImg = Input.IDCardImg.FileName;  // Update this logic to properly handle file uploads
            }

            user.FullName = Input.FullName;
            user.PhoneNumber = Input.PhoneNumber;
            user.Address = Input.Address;
            user.Age = Input.Age;
            user.Sex = Input.Sex;
            user.CreditCardNumber = Input.CreditCardNumber;
            user.IDCard = Input.IDCard;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            _logger.LogInformation("User updated their profile successfully.");
            return RedirectToPage("./Index");
        }
    }
}
