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
        [Route("")]
        public async Task<ActionResult<Enrollment>> Create(Enrollment enrollment)
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
        [Route("")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> List()
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Enrollment>> Find(int id)
        {
            try
            {
                var enrollment = await _context.Enrollment.FindAsync(id);

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

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, Enrollment updatedEnrollment)
        {
            try
            {
                if (id != updatedEnrollment.Id)
                {
                    return BadRequest("Invalid enrollment ID");
                }

                var existingEnrollment = await _context.Enrollment.FindAsync(id);

                if (existingEnrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                existingEnrollment.StudentId = updatedEnrollment.StudentId; 
                existingEnrollment.CourseId = updatedEnrollment.CourseId; 
                existingEnrollment.EnrollmentDate = updatedEnrollment.EnrollmentDate; 

                await _context.SaveChangesAsync();

                return Ok(existingEnrollment);
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
                var enrollment = await _context.Enrollment.FindAsync(id);

                if (enrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                _context.Enrollment.Remove(enrollment);
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