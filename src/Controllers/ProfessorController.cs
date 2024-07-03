using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfessorController : Controller
    {
        private readonly DataContext _context;
        public ProfessorController(DataContext appDbContext) {
            _context = appDbContext;
        }

        [HttpPost]
        public ActionResult<Professor> AddProfessor(Professor professor) 
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Created("", professor);
        }

        [HttpPut]
        public async Task<ActionResult<Professor>> UpdateProfessor(Professor newProfessor) 
        {
            try {
                var professor = await _context.Professor.FindAsync(newProfessor.Id);

                if (professor == null)
                {
                    return NotFound("Professor not found");
                }

                foreach (var property in newProfessor.GetType().GetProperties())
                {
                    var newValue = newProfessor.GetType().GetProperty(property.Name)?.GetValue(newProfessor);
                    property.SetValue(professor, newValue);
                }

                await _context.SaveChangesAsync();

                return Ok(newProfessor);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var professors = await _context.Professor.ToListAsync();

                return Ok(professors);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Professor>> Get(int Id) 
        {
            try {
                var professor = await _context.Professor.FirstOrDefaultAsync(p => p.Id == Id);

                if (professor == null) {
                    return NotFound("Professor not found");
                }
                
                return Ok(professor);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                var professor = await _context.Professor.FindAsync(Id);
                
                if (professor == null)
                {
                    return NotFound("Professor not found");
                }

                _context.Professor.Remove(professor);
                await _context.SaveChangesAsync();

                return Ok("Professor deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}