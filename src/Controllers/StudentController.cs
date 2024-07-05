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
        //GeneralController genCtrl = new();

        public StudentController(DataContext appDbContext) {
            _context = appDbContext;
        }

        //public async Task<Student> BuildStudent(Student _Student)
        //{
        //    _Student.Adress = await genCtrl.GetAdressId(_Student.AdressId);
            
        //    return _Student;
        //}

        //public async Task<Student[]> BuildStudent(Student[] Students)
        //{
        //    foreach (var Student in Students){
        //        Student StudentTemp = await BuildStudent(Student);
        //        Student.Adress = StudentTemp.Adress;
        //    }   
        //    return Students;
        //}

        [HttpPost]
        [Route("")] 
        public async Task<ActionResult<Student>> Create(Student student) 
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return Created("", student);
        }

        [HttpPatch]
        [Route("")]
        public async Task<ActionResult<Student>> Update(Student newStudent) 
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
        [Route("")]
        public async Task<IActionResult> List() 
        {
            try {
                var students = await _context.Student.ToListAsync();

                return Ok(students);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Student>> Find(int id) 
        {
            try {
                var student = await _context.Student.FirstOrDefaultAsync(p => p.Id == id);

                if (student == null) {
                    return NotFound("Student not found");
                }
                
                return Ok(student);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Route("")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var student = await _context.Student.FindAsync(id);
                
                if (student == null)
                {
                    return NotFound("Student not found");
                }

                _context.Student.Remove(student);
                await _context.SaveChangesAsync();

                return Ok();
            } catch {
                return StatusCode(500);
            }
        }
    }
}