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
        GeneralController genCtrl = new();

        public EnrollmentController(DataContext context)
        {
            _context = context;
        }

        public async Task<Enrollment> buildEnrollment(Enrollment enrollment)
        {
            enrollment.Student = await genCtrl.GetStudentId(enrollment.StudentId);
            //Implementar montador de Student.
            enrollment.Course = await genCtrl.GetCourseId(enrollment.CourseId);
            return enrollment;
        }

        public async Task<Enrollment[]> buildEnrollments(Enrollment[] enrollments)
        {
            foreach (var enrollment in enrollments){
                Enrollment enrollmentTemp = await buildEnrollment(enrollment);
                enrollment.Student = await genCtrl.GetStudentId(enrollment.StudentId);
                //Implementar montador de Student.
                enrollment.Course = await genCtrl.GetCourseId(enrollment.CourseId);
            }

            return enrollments;
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
                enrollments = (await buildEnrollments(enrollments.ToArray())).ToList();


                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentById(int id)
        {
            try
            {
                var enrollment = await _context.Enrollment.FindAsync(id);

                if (enrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                enrollment = await buildEnrollment(enrollment);

                return Ok(enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, Enrollment updatedEnrollment)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
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

                return Ok("Enrollment deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}