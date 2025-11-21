namespace S14_Aquino.Dtos.Students
{
    public class StudentRequest
    {
        public int GradeID { get; set; }   // FK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

