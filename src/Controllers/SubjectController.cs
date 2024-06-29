using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("subject")]
    [ApiController]
    public class SubjectController : Controller
    {
        private readonly DataContext _appDbContext;

        public SubjectController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")] 
        public ActionResult<Subject> AddSubject(Subject subject) 
        {
            _appDbContext.Add(subject);
            _appDbContext.SaveChanges();
            return Created("", subject);
        }

        [HttpPut]
        public async Task<ActionResult<Subject>> UpdateSubject(Subject newSubject) 
        {
            try {
                var subject = await _appDbContext.Subjects.FindAsync(newSubject.Id);

                if (subject == null)
                {
                    return NotFound("Subject not found");
                }

                foreach (var property in newSubject.GetType().GetProperties())
                {
                    var newValue = newSubject.GetType().GetProperty(property.Name)?.GetValue(newSubject);
                    property.SetValue(subject, newValue);
                }

                await _appDbContext.SaveChangesAsync();

                return Ok(newSubject);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var subjects = await _appDbContext.Subjects.ToListAsync();

                return Ok(subjects);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            try {
                var subject = await _appDbContext.Subjects.FirstOrDefaultAsync(s => s.Id == id);

                if (subject == null) {
                    return NotFound("Subject not found");
                }
                
                return Ok(subject);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var subject = await _appDbContext.Subjects.FindAsync(id);
                
                if (subject == null)
                {
                    return NotFound("Subject not found");
                }

                _appDbContext.Subjects.Remove(subject);
                await _appDbContext.SaveChangesAsync();

                return Ok("Subject deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}