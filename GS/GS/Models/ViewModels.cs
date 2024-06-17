namespace GS.Models
{
    public class ViewModels
    {
        public GS.Areas.Identity.Pages.Account.LoginModel LoginModel { get; set; }
        public List<Course> Courses
        {
            get; set;
        }
    }
}
