using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S14_Aquino.Dtos.Courses;
using S14_Aquino.Models;

namespace S14_Aquino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseResponse>>> GetCourses()
        {
            var list = await _context.Courses
                .Where(c => c.Active)
                .ToListAsync();

            return Ok(list.Select(c => new CourseResponse
            {
                CourseID = c.CourseID,
                Name = c.Name,
                Credit = c.Credit,
                Active = c.Active
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseResponse>> GetCourse(int id)
        {
            var c = await _context.Courses
                .Where(x => x.CourseID == id && x.Active)
                .FirstOrDefaultAsync();

            if (c == null)
                return NotFound();

            return Ok(new CourseResponse
            {
                CourseID = c.CourseID,
                Name = c.Name,
                Credit = c.Credit,
                Active = c.Active
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CourseRequest request)
        {
            var course = new Course
            {
                Name = request.Name,
                Credit = request.Credit,
                Active = true
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok("Course created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseRequest request)
        {
            var c = await _context.Courses.FindAsync(id);

            if (c == null || !c.Active)
                return NotFound();

            c.Name = request.Name;
            c.Credit = request.Credit;

            await _context.SaveChangesAsync();
            return Ok("Course updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var c = await _context.Courses.FindAsync(id);

            if (c == null || !c.Active)
                return NotFound();

            c.Active = false;
            await _context.SaveChangesAsync();

            return Ok("Course disabled");
        }
    }
}

