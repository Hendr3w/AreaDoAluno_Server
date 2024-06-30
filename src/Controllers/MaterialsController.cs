using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("materials")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly DataContext _context;
        GeneralController genCtrl = new();

        public MaterialsController(DataContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Materials> BuildMaterials(Materials material)
        {
            material.Class = await genCtrl.GetClassId(material.ClassId);
            
            return material;
        }

        public async Task<Materials[]> BuildMaterials(Materials[] materials)
        {
            foreach (var material in materials){
                Materials MaterialsTemp = await BuildMaterials(material);
                material.Class = MaterialsTemp.Class;
                
            }
            return materials;
        }

        [HttpPost]
        [Route("cadastrar")]
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
        [Route("all")]
        public async Task<ActionResult<IEnumerable<Materials>>> GetAllMaterials()
        {
            try
            {
                var materials = await _context.Materials.ToListAsync();
                materials = (await BuildMaterials(materials.ToArray())).ToList();

                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Route("get")]
        public async Task<ActionResult<Materials>> GetMaterialById(int id)
        {
            try
            {
                var material = await _context.Materials.FindAsync(id);

                if (material == null)
                {
                    return NotFound("Material not found");
                }

                material = await BuildMaterials(material);

                return Ok(material);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Route("update")]
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

        [HttpDelete("{id}")]
        [Route("delete")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            try
            {
                var material = await _context.Materials.FindAsync(id);

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