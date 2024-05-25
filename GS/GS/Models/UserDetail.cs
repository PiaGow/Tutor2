namespace GS.Models
{
    public class UserDetail :ApplicationUser
    {
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
        public string? Sex { get; set; }
        public string? CreditCardNumber { get; set; }
        public string? IDCard { get; set; }
        public string? IDCardImg { get; set; }
        public int Idst { get; set; }
        public Subject Subject { get; set; }
        public int Idcs { get; set; }
        public Class Class { get; set; }
        public int IdService { get; set; }
        public Servicer service { get; set; }
    }
}
