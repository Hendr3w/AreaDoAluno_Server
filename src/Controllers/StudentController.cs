using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly DataContext _appDbContext;

        public StudentController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")] 
        public ActionResult<Student> AddStudent(Student student) 
        {
            _appDbContext.Add(student);
            _appDbContext.SaveChanges();
            return Created("", student);
        }

        [HttpPut]
        public async Task<ActionResult<Student>> UpdateStudent(Student newStudent) 
        {
            try {
                var student = await _appDbContext.Students.FindAsync(newStudent.Id);

                if (student == null)
                {
                    return NotFound("Student not found");
                }

                foreach (var property in newStudent.GetType().GetProperties())
                {
                    var newValue = newStudent.GetType().GetProperty(property.Name)?.GetValue(newStudent);
                    property.SetValue(student, newValue);
                }

                await _appDbContext.SaveChangesAsync();

                return Ok(newStudent);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var students = await _appDbContext.Students.ToListAsync();

                return Ok(students);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) 
        {
            try {
                var student = await _appDbContext.Students.FirstOrDefaultAsync(s => s.Id == id);

                if (student == null) {
                    return NotFound("Student not found");
                }
                
                return Ok(student);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                int isStudentDeleted = await _appDbContext.Students.Where(s => s.Id == id).ExecuteDeleteAsync();

                if (isStudentDeleted == 0)
                {
                    return NotFound("Student not found");
                }

                return Ok("Student deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}