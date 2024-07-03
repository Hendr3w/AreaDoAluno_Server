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
        public async Task<IActionResult> GetAll() 
        {
            try {
                var discipline = await _context.Discipline.ToListAsync();

                return Ok(discipline);
            } catch {
                return StatusCode(400);
            }
        }


        [HttpGet("{Id}")]
        public async Task<ActionResult<Discipline>> GetId(int Id)
        {
            try{
                var discipline = await _context.Discipline.FirstOrDefaultAsync(a => a.Id == Id);

                if (discipline == null)
                    return NotFound();
                
                return discipline;
            } catch{
                return StatusCode(500);
            }

        }

        [HttpPost]
        public ActionResult<Discipline> AddDiscipline(Discipline discipline) 
        {
            _context.Add(discipline);
            _context.SaveChanges();
            return Created("", discipline);
        }

        [HttpPut]
        public async Task<ActionResult<Discipline>> UpdateDiscipline(Discipline newDiscipline) 
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                var Discipline = await _context.Discipline.FindAsync(Id);
                
                if (Discipline == null)
                {
                    return NotFound("Discipline not found");
                }

                _context.Discipline.Remove(Discipline);
                await _context.SaveChangesAsync();

                return Ok("Discipline deleted");
            } catch {
                return StatusCode(500);
            }
        }

        
    }
}
