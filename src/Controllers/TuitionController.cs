using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("tuition")]
    [ApiController]
    public class TuitionController : Controller
    {
        private readonly DataContext _appDbContext;

        public TuitionController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")] 
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
                var tuition = await _appDbContext.Tuitions.FindAsync(newTuition.Id);

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
        [Route("all")]
        public async Task<IActionResult> GetAll() 
        {
            try {
                var tuitions = await _appDbContext.Tuitions.ToListAsync();

                return Ok(tuitions);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            try {
                var tuition = await _appDbContext.Tuitions.FirstOrDefaultAsync(t => t.Id == id);

                if (tuition == null) {
                    return NotFound("Tuition not found");
                }
                
                return Ok(tuition);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var tuition = await _appDbContext.Tuitions.FindAsync(id);
                
                if (tuition == null)
                {
                    return NotFound("Tuition not found");
                }

                _appDbContext.Tuitions.Remove(tuition);
                await _appDbContext.SaveChangesAsync();

                return Ok("Tuition deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}