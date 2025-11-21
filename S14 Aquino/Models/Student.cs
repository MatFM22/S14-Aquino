namespace S14_Aquino.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        // Eliminación lógica
        public bool Active { get; set; } = true;

        // Foreign Key
        public int GradeID { get; set; }
        public Grade Grade { get; set; }

        // Relación 1:N con Enrollment
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
