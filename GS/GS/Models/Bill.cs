using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class Bill
    {
        [Key]
        public int IdBill { get; set; } 
        public string? Name { get; set; }
        public DateTime? DateOfPayment { get; set; }
        public float TotalDiscount { get; set; }
        public float? TotalMoney { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
