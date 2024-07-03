using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly DataContext _context;
        public EnrollmentController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Enrollment>> AddEnrollment(Enrollment enrollment)
        {
            try
            {
                _context.Enrollment.Add(enrollment);
                await _context.SaveChangesAsync();
                return Created("", enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAllEnrollment()
        {
            try
            {
                var enrollments = await _context.Enrollment.ToListAsync();


                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentById(int Id)
        {
            try
            {
                var enrollment = await _context.Enrollment.FindAsync(Id);

                if (enrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEnrollment(Enrollment enrollment)
        {
            try
            {
                var existingEnrollment = await _context.Enrollment.FindAsync(enrollment.Id);

                if (existingEnrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                existingEnrollment.StudentId = enrollment.StudentId; 
                existingEnrollment.CourseId = enrollment.CourseId; 
                existingEnrollment.EnrollmentDate = enrollment.EnrollmentDate; 

                await _context.SaveChangesAsync();

                return Ok(existingEnrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEnrollment(int Id)
        {
            try
            {
                var enrollment = await _context.Enrollment.FindAsync(Id);

                if (enrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                _context.Enrollment.Remove(enrollment);
                await _context.SaveChangesAsync();

                return Ok("Enrollment deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}