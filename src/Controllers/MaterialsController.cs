using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly DataContext _context;
        public MaterialsController(DataContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Materials>> AddMaterial(Materials material)
        {
            try
            {
                _context.Materials.Add(material);
                await _context.SaveChangesAsync();
                return Created("", material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materials>>> GetAllMaterials()
        {
            try
            {
                var materials = await _context.Materials.ToListAsync();

                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Materials>> GetMaterialById(int Id)
        {
            try
            {
                var material = await _context.Materials.FindAsync(Id);

                if (material == null)
                {
                    return NotFound("Material not found");
                }

                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaterial(int id, Materials updatedMaterial)
        {
            try
            {
                if (id != updatedMaterial.Id)
                {
                    return BadRequest("Invalid material ID");
                }

                var existingMaterial = await _context.Materials.FindAsync(id);

                if (existingMaterial == null)
                {
                    return NotFound("Material not found");
                }

                existingMaterial.Name = updatedMaterial.Name; 
                existingMaterial.ClassId = updatedMaterial.ClassId; 
                existingMaterial.Description = updatedMaterial.Description;

                await _context.SaveChangesAsync();

                return Ok(existingMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMaterial(int Id)
        {
            try
            {
                var material = await _context.Materials.FindAsync(Id);

                if (material == null)
                {
                    return NotFound("Material not found");
                }

                _context.Materials.Remove(material);
                await _context.SaveChangesAsync();

                return Ok("Material deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}