using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TuitionController : Controller
    {
        private readonly DataContext _appDbContext;
        public TuitionController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public ActionResult<Tuition> AddTuition(Tuition tuition) 
        {
            _appDbContext.Add(tuition);
            _appDbContext.SaveChanges();
            return Created("", tuition);
        }

        [HttpPut]
        public async Task<ActionResult<Tuition>> UpdateTuition(Tuition newTuition) 
        {
            try {
                var tuition = await _appDbContext.Tuition.FindAsync(newTuition.Id);

                if (tuition == null)
                {
                    return NotFound("Tuition not found");
                }

                foreach (var property in newTuition.GetType().GetProperties())
                {
                    var newValue = newTuition.GetType().GetProperty(property.Name)?.GetValue(newTuition);
                    property.SetValue(tuition, newValue);
                }

                await _appDbContext.SaveChangesAsync();

                return Ok(newTuition);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var tuitions = await _appDbContext.Tuition.ToListAsync();

                return Ok(tuitions);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id) 
        {
            try {
                var tuition = await _appDbContext.Tuition.FirstOrDefaultAsync(t => t.Id == Id);

                if (tuition == null) {
                    return NotFound("Tuition not found");
                }

                return Ok(tuition);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id) 
        {
            try {
                var tuition = await _appDbContext.Tuition.FindAsync(Id);
                
                if (tuition == null)
                {
                    return NotFound("Tuition not found");
                }

                _appDbContext.Tuition.Remove(tuition);
                await _appDbContext.SaveChangesAsync();

                return Ok("Tuition deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}