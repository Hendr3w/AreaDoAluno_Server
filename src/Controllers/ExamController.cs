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
        GeneralController genCtrl = new();

        public ExamController(DataContext context)
        {
            _context = context;
        }

        public async Task<Exam> BuildExam(Exam _Exam)
        {
            _Exam.Class = await genCtrl.GetClassId(_Exam.ClassId);
            
            return _Exam;
        }

        public async Task<Exam[]> BuildCExams(Exam[] _Exames)
        {
            foreach (var _Exam in _Exames){
                Exam ExamTemp = await BuildExam(_Exam);
                _Exam.Class = ExamTemp.Class;
            }
            return _Exames;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> GetExamById(int id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, Exam updatedExam)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(int id)
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

                return Ok("Exam deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}