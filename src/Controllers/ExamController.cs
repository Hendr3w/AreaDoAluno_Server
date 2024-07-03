using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly Data.DataContext _context;
        public ExamController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Exam>> AddExam(Exam exam)
        {
            try
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return Created("", exam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exam>>> GetAll()
        {
            try
            {
                var exam  = await _context.Exam.ToListAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Exam>> GetExamById(int Id)
        {
            try
            {
                var exam = await _context.Exam.FindAsync(Id);

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

        [HttpPut]
        public async Task<IActionResult> UpdateExam(Exam updatedExam)
        {
            try
            {
                var existingExam = await _context.Exam.FindAsync(updatedExam.Id);

                if (existingExam == null)
                {
                    return NotFound("Exam not found");
                }

                existingExam.ClassId = updatedExam.ClassId;
                existingExam.Deadline = updatedExam.Deadline;
                await _context.SaveChangesAsync();

                return Ok(existingExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteExam(int Id)
        {
            try
            {
                var exam = await _context.Exam.FindAsync(Id);

                if (exam == null)
                {
                    return NotFound("Exam not found");
                }

                _context.Remove(exam);
                await _context.SaveChangesAsync();

                return Ok("Exam deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}