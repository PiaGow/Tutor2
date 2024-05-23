using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public string? PhoneNumber {  get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
        public string? Sex { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? IDCard { get; set; }
        public string? IDCardImg { get; set; }
        public int Idst { get; set; }
        public Subject Subject { get; set; }
        public int Idcs { get; set; }
        public Class Class { get; set; }
        public int IdService { get; set; }
        public Servicer service { get; set; }
        public string UserRole { get; set; }
		[ForeignKey("UserRole")]
		[ValidateNever]
		public IdentityRole? IdentityRole { get; set; }
	}
}
