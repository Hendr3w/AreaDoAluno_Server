using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly DataContext _appDbContext;

        public EnrollmentController(DataContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult<Enrollment>> AddEnrollment(Enrollment enrollment)
        {
            try
            {
                _appDbContext.Enrollments.Add(enrollment);
                await _appDbContext.SaveChangesAsync();
                return Created("", enrollment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Enrollment>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _appDbContext.Enrollments.ToListAsync();
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
                var enrollment = await _appDbContext.Enrollments.FindAsync(id);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrollment(int id, Enrollment updatedEnrollment)
        {
            try
            {
                if (id != updatedEnrollment.Id)
                {
                    return BadRequest("Invalid enrollment ID");
                }

                var existingEnrollment = await _appDbContext.Enrollments.FindAsync(id);

                if (existingEnrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                existingEnrollment.StudentId = updatedEnrollment.StudentId; // Assuming 'StudentId' is a property to update
                existingEnrollment.CourseId = updatedEnrollment.CourseId; // Assuming 'CourseId' is a property to update
                existingEnrollment.EnrollmentDate = updatedEnrollment.EnrollmentDate; // Assuming 'EnrollmentDate' is a property to update

                await _appDbContext.SaveChangesAsync();

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
                var enrollment = await _appDbContext.Enrollments.FindAsync(id);

                if (enrollment == null)
                {
                    return NotFound("Enrollment not found");
                }

                _appDbContext.Enrollments.Remove(enrollment);
                await _appDbContext.SaveChangesAsync();

                return Ok("Enrollment deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}