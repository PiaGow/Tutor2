namespace GS.Models
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public Course? Course { get; set; }
        public Subject? Subject { get; set; }
    }
}
