using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class Ratting
    {
        public int Id { get; set; }
        public float CourseNumber { get; set; }
        public string ReviewNumber { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
