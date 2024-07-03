using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        public StudentController(DataContext appDbContext) {
            _context = appDbContext;
        }

        [HttpPost]
        public ActionResult<Student> AddStudent(Student Student) 
        {
            _context.Add(Student);
            _context.SaveChanges();
            return Created("", Student);
        }

        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudent(Student newStudent) 
        {
            try {
                var Student = await _context.Student.FindAsync(newStudent.Id);

                if (Student == null)
                {
                    return NotFound("Student not found");
                }

                foreach (var property in newStudent.GetType().GetProperties())
                {
                    var newValue = newStudent.GetType().GetProperty(property.Name)?.GetValue(newStudent);
                    property.SetValue(Student, newValue);
                }

                await _context.SaveChangesAsync();

                return Ok(newStudent);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var Students = await _context.Student.ToListAsync();
                Console.WriteLine("Students: ", Students);

                return Ok(Students);
            } catch (Exception e){
                Console.WriteLine(e.Message);
                return StatusCode(400);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Student>> Get(int Id) 
        {
            try {
                var Student = await _context.Student.FirstOrDefaultAsync(p => p.Id == Id);

                if (Student == null) {
                    return NotFound("Student not found");
                }
                
                return Ok(Student);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                var Student = await _context.Student.FindAsync(Id);
                
                if (Student == null)
                {
                    return NotFound("Student not found");
                }

                _context.Student.Remove(Student);
                await _context.SaveChangesAsync();

                return Ok("Student deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}