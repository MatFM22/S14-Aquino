namespace S14_Aquino.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public DateTime Date { get; set; }

        // Eliminación lógica
        public bool Active { get; set; } = true;

        // Foreign keys
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
