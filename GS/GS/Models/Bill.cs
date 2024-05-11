using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class Bill
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public float? TotalMoney { get; set; }
        public string IdUser { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
