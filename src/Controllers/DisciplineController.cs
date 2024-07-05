using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DisciplineController : Controller
    {
        private readonly DataContext _context;

        public DisciplineController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List() 
        {
            try {
                var discipline = await _context.Discipline.ToListAsync();

                return Ok(discipline);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Discipline>> Find(int id)
        {
            try{
                var discipline = await _context.Discipline.FirstOrDefaultAsync(a => a.Id == id);

                if (discipline == null)
                    return NotFound();
                
                return discipline;
            } catch{
                return StatusCode(500);
            }

        }

        [HttpPost]
        [Route("")] 
        public ActionResult<Discipline> Create(Discipline discipline) 
        {
            _context.Add(discipline);
            _context.SaveChanges();
            return Created("", discipline);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Discipline>> Update(Discipline newDiscipline) 
        {
            try {
                var discipline = await _context.Discipline.FindAsync(newDiscipline.Id);

                if (discipline == null)
                {
                    return NotFound("Discipline not found");
                }

                foreach (var property in newDiscipline.GetType().GetProperties())
                {
                    var newValue = newDiscipline.GetType().GetProperty(property.Name)?.GetValue(newDiscipline);
                    property.SetValue(discipline, newValue);
                }

                await _context.SaveChangesAsync();

                return Ok(newDiscipline);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var Discipline = await _context.Discipline.FindAsync(id);
                
                if (Discipline == null)
                {
                    return NotFound("Discipline not found");
                }

                _context.Discipline.Remove(Discipline);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            } catch {
                return StatusCode(500);
            }
        }

        
    }
}
