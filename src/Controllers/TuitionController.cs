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
        [Route("")] 
        public ActionResult<Tuition> Create(Tuition tuition) 
        {
            _appDbContext.Add(tuition);
            _appDbContext.SaveChanges();
            return Created("", tuition);
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Tuition>> Update(Tuition newTuition) 
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
        [Route("")]
        public async Task<IActionResult> List() 
        {
            try {
                var tuitions = await _appDbContext.Tuition.ToListAsync();

                return Ok(tuitions);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Find(int id) 
        {
            try {
                var tuition = await _appDbContext.Tuition.FirstOrDefaultAsync(t => t.Id == id);

                if (tuition == null) {
                    return NotFound("Tuition not found");
                }

                return Ok(tuition);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var tuition = await _appDbContext.Tuition.FindAsync(id);
                
                if (tuition == null)
                {
                    return NotFound("Tuition not found");
                }

                _appDbContext.Tuition.Remove(tuition);
                await _appDbContext.SaveChangesAsync();

                return StatusCode(204);
            } catch {
                return StatusCode(500);
            }
        }
    }
}