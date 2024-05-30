using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.Models
{
    public class TimeCourse
    {
        [Key]
        public int Idtimece { get; set; }
        [DisplayName("Giờ Học")]
        public TimeOnly? Timestart { get; set; }
        [DisplayName("Giờ Kết Thúc")]
        public TimeOnly? Timeend { get; set; }
    }
}
