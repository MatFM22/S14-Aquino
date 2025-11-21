using S14_Aquino.Dtos.Grades;

namespace S14_Aquino.Dtos.Students
{
    public class StudentResponse
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public GradeResponse Grade { get; set; }
    }
}
