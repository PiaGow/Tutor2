using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GS.Models
{
	public class CoursesStudent
	{
		[Key]
		public int Idcourses { get; set; }

		public string UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public ApplicationUser? ApplicationUser { get; set; }

		public int Idce { get; set; }
		[ForeignKey("Idce")]
		[ValidateNever]
		public Course? Course { get; set; }
        public List<HomeWork> HomeworkList { get; set; }
    }
}
