using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S14_Aquino.Dtos.Enrollments;
using S14_Aquino.Dtos.Courses;
using S14_Aquino.Dtos.Students;
using S14_Aquino.Models;

namespace S14_Aquino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentResponse>>> GetEnrollments()
        {
            var list = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .Where(e => e.Student.Active && e.Course.Active)
                .ToListAsync();

            return Ok(list.Select(e => new EnrollmentResponse
            {
                EnrollmentID = e.EnrollmentId,
                Date = e.Date,
                Student = new StudentResponse
                {
                    StudentID = e.Student.StudentID,
                    FullName = $"{e.Student.FirstName} {e.Student.LastName}",
                    Email = e.Student.Email,
                    Phone = e.Student.Phone,
                },
                Course = new CourseResponse
                {
                    CourseID = e.Course.CourseID,
                    Name = e.Course.Name,
                    Credit = e.Course.Credit,
                    Active = e.Course.Active
                }
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(EnrollmentRequest request)
        {
            var enrollment = new Enrollment
            {
                StudentID = request.StudentID,
                CourseID = request.CourseID,
                Date = request.Date
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok("Enrollment created");
        }
    }
}
