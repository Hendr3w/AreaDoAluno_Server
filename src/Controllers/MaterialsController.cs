using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("materials")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly DataContext _appDbContext;

        public MaterialsController(DataContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult<Material>> AddMaterial(Material material)
        {
            try
            {
                _appDbContext.Materials.Add(material);
                await _appDbContext.SaveChangesAsync();
                return Created("", material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterials()
        {
            try
            {
                var materials = await _appDbContext.Materials.ToListAsync();
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterialById(int id)
        {
            try
            {
                var material = await _appDbContext.Materials.FindAsync(id);

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
        public async Task<IActionResult> UpdateMaterial(int id, Material updatedMaterial)
        {
            try
            {
                if (id != updatedMaterial.Id)
                {
                    return BadRequest("Invalid material ID");
                }

                var existingMaterial = await _appDbContext.Materials.FindAsync(id);

                if (existingMaterial == null)
                {
                    return NotFound("Material not found");
                }

                existingMaterial.Title = updatedMaterial.Title; // Assuming 'Title' is a property to update
                existingMaterial.Description = updatedMaterial.Description; // Assuming 'Description' is a property to update
                existingMaterial.Content = updatedMaterial.Content; // Assuming 'Content' is a property to update

                await _appDbContext.SaveChangesAsync();

                return Ok(existingMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            try
            {
                var material = await _appDbContext.Materials.FindAsync(id);

                if (material == null)
                {
                    return NotFound("Material not found");
                }

                _appDbContext.Materials.Remove(material);
                await _appDbContext.SaveChangesAsync();

                return Ok("Material deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}