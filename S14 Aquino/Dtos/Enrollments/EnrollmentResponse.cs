using S14_Aquino.Dtos.Courses;
using S14_Aquino.Dtos.Students;

namespace S14_Aquino.Dtos.Enrollments
{
    public class EnrollmentResponse
    {
        public int EnrollmentID { get; set; }
        public DateTime Date { get; set; }

        public StudentResponse Student { get; set; }
        public CourseResponse Course { get; set; }
    }
}
