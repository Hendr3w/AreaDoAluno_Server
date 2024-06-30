using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("Student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly DataContext _context;
        GeneralController genCtrl = new();

        public StudentController(DataContext appDbContext) {
            _context = appDbContext;
        }


        public async Task<Student> BuildStudent(Student _Student)
        {
            _Student.Adress = await genCtrl.GetAdressId(_Student.AdressId);
            
            return _Student;
        }

        public async Task<Student[]> BuildStudent(Student[] Students)
        {
            foreach (var Student in Students){
                Student StudentTemp = await BuildStudent(Student);
                Student.Adress = StudentTemp.Adress;
            }   
            return Students;
        }




        [HttpPost]
        [Route("cadastrar")] 
        public ActionResult<Student> AddStudent(Student Student) 
        {
            _context.Add(Student);
            _context.SaveChanges();
            return Created("", Student);
        }

        [HttpPut]
        [Route("update")]
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
        [Route("all")]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var Students = await _context.Student.ToListAsync();
                Students = (await BuildStudent(Students.ToArray())).ToList();

                return Ok(Students);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{id}")]
        [Route("get")]
        public async Task<ActionResult<Student>> Get(int id) 
        {
            try {
                var Student = await _context.Student.FirstOrDefaultAsync(p => p.Id == id);

                if (Student == null) {
                    return NotFound("Student not found");
                }
                Student = await BuildStudent(Student);
                
                return Ok(Student);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var Student = await _context.Student.FindAsync(id);
                
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