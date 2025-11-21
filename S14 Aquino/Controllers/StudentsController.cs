using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S14_Aquino.Dtos.Students;
using S14_Aquino.Dtos.Grades;
using S14_Aquino.Models;

namespace S14_Aquino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/students (solo activos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetStudents()
        {
            var students = await _context.Students
                .Where(s => s.Active)
                .Include(s => s.Grade)
                .ToListAsync();

            var result = students.Select(s => new StudentResponse
            {
                StudentID = s.StudentID,
                FullName = $"{s.FirstName} {s.LastName}",
                Email = s.Email,
                Phone = s.Phone,
                Grade = new GradeResponse
                {
                    GradeID = s.Grade.GradeID,
                    Name = s.Grade.Name,
                    Description = s.Grade.Description
                }
            });

            return Ok(result);
        }

        // GET api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponse>> GetStudent(int id)
        {
            var student = await _context.Students
                .Where(s => s.Active && s.StudentID == id)
                .Include(s => s.Grade)
                .FirstOrDefaultAsync();

            if (student == null)
                return NotFound("Student not found");

            var result = new StudentResponse
            {
                StudentID = student.StudentID,
                FullName = $"{student.FirstName} {student.LastName}",
                Email = student.Email,
                Phone = student.Phone,
                Grade = new GradeResponse
                {
                    GradeID = student.Grade.GradeID,
                    Name = student.Grade.Name,
                    Description = student.Grade.Description
                }
            };

            return Ok(result);
        }

        // POST api/students
        [HttpPost]
        public async Task<ActionResult> CreateStudent(StudentRequest request)
        {
            var student = new Student
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                GradeID = request.GradeID,
                Active = true
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok("Student created successfully");
        }

        // PUT api/students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentRequest request)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null || !student.Active)
                return NotFound("Student not found");

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.Phone = request.Phone;
            student.GradeID = request.GradeID;

            await _context.SaveChangesAsync();

            return Ok("Student updated successfully");
        }

        // DELETE lógico api/students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null || !student.Active)
                return NotFound("Student not found");

            student.Active = false;
            await _context.SaveChangesAsync();

            return Ok("Student disabled (logical delete)");
        }
    }
}


