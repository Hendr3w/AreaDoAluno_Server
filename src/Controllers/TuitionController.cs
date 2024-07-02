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
        GeneralController genCtrl = new();

        public TuitionController(DataContext appDbContext) {
            _appDbContext = appDbContext;
        }

         public async Task<Tuition> BuildTuition(Tuition _Tuition)
        {
            _Tuition.Enrollment = await genCtrl.GetEnrollmentId(_Tuition.EnrollmentId);
            
            return _Tuition;
        }

        public async Task<Tuition[]> BuildTuition(Tuition[] _Tuitiones)
        {
            foreach (var _Tuition in _Tuitiones){
                Tuition TuitionTemp = await BuildTuition(_Tuition);
                 _Tuition.Enrollment = await genCtrl.GetEnrollmentId(_Tuition.EnrollmentId);
            }
            return _Tuitiones;
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
                tuitions = (await BuildTuition(tuitions.ToArray())).ToList();


                return Ok(tuitions);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            try {
                var tuition = await _appDbContext.Tuition.FirstOrDefaultAsync(t => t.Id == id);

                if (tuition == null) {
                    return NotFound("Tuition not found");
                }
                
                tuition = await BuildTuition(tuition);

                return Ok(tuition);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
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

                return Ok("Tuition deleted");
            } catch {
                return StatusCode(500);
            }
        }
    }
}