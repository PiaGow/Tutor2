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
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
        public string? Sex { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? IDCard { get; set; }
        public string? IDCardImg { get; set; }
        public string? ValidUser { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Class> Classes { get; set; }
        public List<Servicer> service { get; set; }



    }
}
