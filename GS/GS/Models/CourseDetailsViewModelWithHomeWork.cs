namespace GS.Models
{
    public class CourseDetailsViewModelWithHomeWork
    {
        public Course Course { get; set; }
        public List<HomeWork> HomeworkList { get; set; }

        public string CreateHomeworkForm(int idce)
        {
            return $"<form method=\"get\" action=\"/HomeWork/Create/{idce}\"><button type=\"submit\" class=\"btn btn-success\">Create Homework</button></form>";
        }
    }
}
