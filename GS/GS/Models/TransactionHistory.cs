using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Models
{
    public class TransactionHistory
    {
        [Key]
        public int IdTH { get; set; }
        public string? Receiver { get; set; }
        public DateTime Date { get; set; }
        public int IdBill { get; set; }
        [ForeignKey("IdBill")]
        [ValidateNever]
        public Bill? Bill { get; set; }


    }
}
