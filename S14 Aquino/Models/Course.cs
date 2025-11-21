namespace S14_Aquino.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
