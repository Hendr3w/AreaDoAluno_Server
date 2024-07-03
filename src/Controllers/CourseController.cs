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
        public async Task<ActionResult<Course>> AddCourseAsync(Course course) 
        {
            _context.Add(course);
            await _context.SaveChangesAsync();
            return Created("", course);
        }


        [HttpPut]
        public async Task<ActionResult<Course>> UpdateCourse(Course NewCourse) 
        {
            try {
                var course = await _context.Course.FindAsync(NewCourse.Id);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                foreach (var property in NewCourse.GetType().GetProperties())
                {
                    var NewValue = NewCourse.GetType().GetProperty(property.Name)?.GetValue(NewCourse);
                    property.SetValue(course, NewValue);
                }

                await _context.SaveChangesAsync();

                return Ok(NewCourse);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var courses = await _context.Course.ToListAsync();

                return Ok(courses);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetId(int Id) 
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                int isCourseDeleted = await _context.Course.Where((course) => course.Id == Id).ExecuteDeleteAsync();

                if (isCourseDeleted == 0)
                {
                    return NotFound("Course not found");
                }

                await _context.Course.Where((course) => course.Id == Id).ExecuteDeleteAsync();

                return Ok("Course deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}
