﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.Models
{
    public class HomeWork
    {
        [Key]
        public int Idhk { get; set; }
        [DisplayName("Tên Bài Tập")]
        public string? Namehk { get; set; }
        [DisplayName("Mô Tả")]
        public string? Status { get; set; }
        [DisplayName("Thời Gian Bắt Đầu")]
        public DateTime? Timestart { get; set; }
        [DisplayName("Hạn Nộp")]
        public DateTime? Timeend { get; set; }
        [DisplayName("Trạng Thái Bài Tập")]
        public string? Assignmentsubmitted { get; set; }
        
        [DisplayName("Thời Gian Nộp")]
        public string? TimeSubmitted { get; set; }
        public int Idce { get; set; }
        public Course Course { get; set; }


    }
}
