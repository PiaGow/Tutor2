using System.ComponentModel.DataAnnotations;

namespace GS.Models
{
    public class Servicer
    {
        [Key]
        public int IdService { get; set; }
        public string Name { get; set; }
        public string Discount { get; set; }
    }
}
