using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.Models
{
    public class Subject
    {
        [Key]
        public int Idst { get; set; }
        [DisplayName("Tên Môn Học")]
        public string? Namest { get; set; }
        public List<Course> courses { get; set; }
        public List<Class> classes { get; set; }
        public List<ApplicationUser> applications { get; set; }
    }
}
