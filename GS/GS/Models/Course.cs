using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GS.Models
{
    public class Course
    {
        [Key]
        public int Idce { get; set; }
        [DisplayName("Tên Khóa Học")]
        public string NameCourse { get; set; }
        [DisplayName("Ngày Bắt Đầu")]
        public DateTime? Starttime { get; set; }
        [DisplayName("Ngày Kết Thúc")]

        public DateTime? Endtime { get; set; }
        [DisplayName("Thông Tin Khóa Học")]

        public string? Courseinformation { get; set; }
        [DisplayName("Ngày Học")]
        public string DayInWeek { get; set; }
        [DisplayName("Ảnh Minh Họa")]
        public string? CourseImg { get; set; }
        
        public string ClassLink { get; set; }
        public float Price { get; set; }
        public int Idst { get; set; }
        [ForeignKey("Idst")]
        [ValidateNever]
        public Subject Subject { get; set; }
        public int Idtimece { get; set; }
        [ForeignKey("Idtimece")]
        [ValidateNever]
        public TimeCourse TimeCourse { get; set; }
        public int Idcs { get; set; }
        [ForeignKey("Idcs")]
        [ValidateNever]
        public required Class Class { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }
        //public ICollection<HomeWork> HomeWorks { get; set; }
    }
}
