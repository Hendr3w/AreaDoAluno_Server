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
        [Route("")] 
        public ActionResult<Professor> Create(Professor professor) 
        {
            _context.Add(professor);
            _context.SaveChanges();
            return Created("", professor);
        }

        [HttpPatch]
        [Route("")]
        public async Task<ActionResult<Professor>> Update(Professor newProfessor) 
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
        [Route("")]
        public async Task<IActionResult> List() 
        {
            try {
                var professors = await _context.Professor.ToListAsync();

                return Ok(professors);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Professor>> Find(int id) 
        {
            try {
                var professor = await _context.Professor.FirstOrDefaultAsync(p => p.Id == id);

                if (professor == null) {
                    return NotFound("Professor not found");
                }
                
                return Ok(professor);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var professor = await _context.Professor.FindAsync(id);
                
                if (professor == null)
                {
                    return NotFound("Professor not found");
                }

                _context.Professor.Remove(professor);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            } catch {
                return StatusCode(500);
            }
        }
    }
}