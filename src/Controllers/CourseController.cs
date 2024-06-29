using AreaDoAluno.Models;
using AreaDoAluno.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("course")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly DataContext _appDbContext;

        public CourseController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")] 
        public ActionResult<Course> AddCourse(Course course) 
        {
            _appDbContext.Add(course);
            _appDbContext.SaveChanges();
            return Created("", course);
        }

        [HttpPut]
        public async Task<ActionResult<Course>> UpdateCourse(Course NewCourse) 
        {
            try {
                var course = await _appDbContext.Course.FindAsync(NewCourse.Id);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                foreach (var property in NewCourse.GetType().GetProperties())
                {
                    var NewValue = NewCourse.GetType().GetProperty(property.Name)?.GetValue(NewCourse);
                    property.SetValue(course, NewValue);
                }

                await _appDbContext.SaveChangesAsync();

                return Ok(NewCourse);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var courses = await _appDbContext.Course.ToListAsync();

                return Ok(courses);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int Id) 
        {
            try {
                var course = await _appDbContext.Course.FirstOrDefaultAsync((course) => course.Id == Id);

                if (course == null) {
                    return NotFound("Course not found");
                }
                
                return Ok(course);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                int isCourseDeleted = await _appDbContext.Course.Where((course) => course.Id == Id).ExecuteDeleteAsync();

                if (isCourseDeleted == 0)
                {
                    return NotFound("Course not found");
                }

                await _appDbContext.Course.Where((course) => course.Id == Id).ExecuteDeleteAsync();

                return Ok("Course deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}
