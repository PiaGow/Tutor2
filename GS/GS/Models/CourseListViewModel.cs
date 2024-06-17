namespace GS.Models
{
    public class CourseListViewModel
    {
        public List<Course> Courses { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
