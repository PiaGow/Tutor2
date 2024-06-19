using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class HomeWorkStudent
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Trạng Thái Bài Tập")]
        public string? Assignmentsubmitted { get; set; }
        [DisplayName("Bài Nộp")]
        public string Assigmented {  get; set; }
        [DisplayName("Thời Gian Nộp")]
        public string? TimeSubmitted { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        public int Idhk { get; set; }    
        [ForeignKey("Idhk")]
        [ValidateNever]
        public HomeWork HomeWork { get; set; }

    }
}
