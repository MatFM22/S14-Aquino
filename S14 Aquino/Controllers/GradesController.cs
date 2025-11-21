using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S14_Aquino.Dtos.Grades;
using S14_Aquino.Models;

namespace S14_Aquino.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GradesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradeResponse>>> GetGrades()
        {
            var list = await _context.Grades.ToListAsync();

            return Ok(list.Select(g => new GradeResponse
            {
                GradeID = g.GradeID,
                Name = g.Name,
                Description = g.Description
            }));
        }

        [HttpPost]
        public async Task<ActionResult> CreateGrade(GradeRequest request)
        {
            var g = new Grade
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Grades.Add(g);
            await _context.SaveChangesAsync();

            return Ok("Grade created");
        }
    }
}
