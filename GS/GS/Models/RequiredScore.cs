using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class RequiredScore
    {
        public int Id { get; set; }
        public float GPA { get; set; }
        public string Achievements { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
