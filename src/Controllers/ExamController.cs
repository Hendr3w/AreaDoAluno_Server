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
        [Route("")]
        public async Task<ActionResult<Exam>> Create(Exam exam)
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
        [Route("")]
        public async Task<ActionResult<IEnumerable<Exam>>> List()
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Exam>> Find(int id)
        {
            try
            {
                var exam = await _context.Exam.FindAsync(id);

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
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, Exam updatedExam)
        {
            try
            {
                if (id != updatedExam.Id)
                {
                    return BadRequest("Invalid exam ID");
                }

                var existingExam = await _context.Exam.FindAsync(id);

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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var exam = await _context.Exam.FindAsync(id);

                if (exam == null)
                {
                    return NotFound("Exam not found");
                }

                _context.Remove(exam);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}