using System.ComponentModel.DataAnnotations;

namespace GS.Models
{
    public class TransactionHistory
    {
        [Key]
        public int IdTH { get; set; }
        public string? Receiver { get; set; }
        public DateTime Date { get; set; }
        public int IdBill { get; set; }
        public Bill? Bill { get; set; }


    }
}
