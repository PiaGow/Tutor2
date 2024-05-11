using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.Models
{
    public class TimeCourse
    {
        [Key]
        public int Idtimece { get; set; }
        [DisplayName("Giờ Học")]
        public DateTime? Timestart { get; set; }
        [DisplayName("Giờ Kết Thúc")]
        public DateTime? Timeend { get; set; }
    }
}
