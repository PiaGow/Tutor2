using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class HomeWork
    {
        [Key]
        public int Idhk { get; set; }
        [DisplayName("Tên Bài Tập")]
        public string? Namehk { get; set; }
        
        [DisplayName("Thời Gian Bắt Đầu")]
        public DateTime? Timestart { get; set; }
        [DisplayName("Hạn Nộp")]
        public DateTime? Timeend { get; set; }
        [DisplayName("Mô Tả")]
        public string Details { get; set; }
        public int Idce { get; set; }
        [ForeignKey("Idce")]
        [ValidateNever]
        public Course Course { get; set; }

    }
}
