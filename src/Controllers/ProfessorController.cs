using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("professor")]
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly DataContext _appDbContext;

        public ProfessorController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")] 
        public ActionResult<Professor> AddProfessor(Professor professor) 
        {
            _appDbContext.Add(professor);
            _appDbContext.SaveChanges();
            return Created("", professor);
        }

        [HttpPut]
        public async Task<ActionResult<Professor>> UpdateProfessor(Professor newProfessor) 
        {
            try {
                var professor = await _appDbContext.Professors.FindAsync(newProfessor.Id);

                if (professor == null)
                {
                    return NotFound("Professor not found");
                }

                foreach (var property in newProfessor.GetType().GetProperties())
                {
                    var newValue = newProfessor.GetType().GetProperty(property.Name)?.GetValue(newProfessor);
                    property.SetValue(professor, newValue);
                }

                await _appDbContext.SaveChangesAsync();

                return Ok(newProfessor);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var professors = await _appDbContext.Professors.ToListAsync();

                return Ok(professors);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            try {
                var professor = await _appDbContext.Professors.FirstOrDefaultAsync(p => p.Id == id);

                if (professor == null) {
                    return NotFound("Professor not found");
                }
                
                return Ok(professor);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var professor = await _appDbContext.Professors.FindAsync(id);
                
                if (professor == null)
                {
                    return NotFound("Professor not found");
                }

                _appDbContext.Professors.Remove(professor);
                await _appDbContext.SaveChangesAsync();

                return Ok("Professor deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}
