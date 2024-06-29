using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly DataContext _appDbContext;

        public ExamController(DataContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult<Exam>> AddExam(Exam exam)
        {
            try
            {
                _appDbContext.Exams.Add(exam);
                await _appDbContext.SaveChangesAsync();
                return Created("", exam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Exam>>> GetAllExams()
        {
            try
            {
                var exams = await _appDbContext.Exams.ToListAsync();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExamById(int id)
        {
            try
            {
                var exam = await _appDbContext.Exams.FindAsync(id);

                if (exam == null)
                {
                    return NotFound("Exam not found");
                }

                return Ok(exam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, Exam updatedExam)
        {
            try
            {
                if (id != updatedExam.Id)
                {
                    return BadRequest("Invalid exam ID");
                }

                var existingExam = await _appDbContext.Exams.FindAsync(id);

                if (existingExam == null)
                {
                    return NotFound("Exam not found");
                }

                existingExam.Title = updatedExam.Title; // Assuming 'Title' is a property to update
                existingExam.Description = updatedExam.Description; // Assuming 'Description' is a property to update
                existingExam.Date = updatedExam.Date; // Assuming 'Date' is a property to update

                await _appDbContext.SaveChangesAsync();

                return Ok(existingExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
        {
            try
            {
                var exam = await _appDbContext.Exams.FindAsync(id);

                if (exam == null)
                {
                    return NotFound("Exam not found");
                }

                _appDbContext.Exams.Remove(exam);
                await _appDbContext.SaveChangesAsync();

                return Ok("Exam deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}