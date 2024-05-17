using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class Assess
    {
        [Key]
        public int IdAS { get; set; }
        public string? Content { get; set; }
        public DateTime TimeAS { get; set; }
        public string? RoleAS { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
