using AreaDoAluno.Models;
using AreaDoAluno.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly DataContext _context;

        public CourseController(DataContext context) 
        {
            _context = context;
        }

        [HttpPost]
        [Route("")] 
        public async Task<ActionResult<Course>> Create(Course course) 
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return Created("", course);
        }

        [HttpPatch]
        [Route("")]
        public async Task<ActionResult<Course>> Update(Course newCourse) 
        {
            try {
                var course = await _context.Course.FindAsync(newCourse.Id);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                foreach (var property in newCourse.GetType().GetProperties())
                {
                    var newValue = newCourse.GetType().GetProperty(property.Name)?.GetValue(newCourse);
                    property.SetValue(course, newValue);
                }

                await _context.SaveChangesAsync();

                return Ok(newCourse);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List() 
        {
            try {
                var courses = await _context.Course.ToListAsync();

                return Ok(courses);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Find(int Id) 
        {
            try {
                var course = await _context.Course.FirstOrDefaultAsync((course) => course.Id == Id);

                if (course == null) {
                    return NotFound("Course not found");
                }
                
                return Ok(course);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                int isCourseDeleted = await _context.Course.Where((course) => course.Id == Id).ExecuteDeleteAsync();

                if (isCourseDeleted == 0)
                {
                    return NotFound("Course not found");
                }

                await _context.Course.Where((course) => course.Id == Id).ExecuteDeleteAsync();

                return StatusCode(204);
            } catch {
                return StatusCode(500);
            }
        }
    }
}
