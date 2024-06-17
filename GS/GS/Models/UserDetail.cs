namespace GS.Models
{
    public class UserDetail :ApplicationUser
    {
        
        public int Idst { get; set; }
        public Subject Subject { get; set; }
        public int Idcs { get; set; }
        public Class Class { get; set; }
        public int IdService { get; set; }
        public Servicer service { get; set; }
    }
}
