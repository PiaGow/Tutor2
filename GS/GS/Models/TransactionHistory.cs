namespace GS.Models
{
    public class TransactionHistory
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public DateTime Date { get; set; }
        public int BillId { get; set; }
        public Bill? Bill { get; set; }


    }
}
