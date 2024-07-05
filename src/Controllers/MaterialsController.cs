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
        //GeneralController genCtrl = new();

        public MaterialsController(DataContext appDbContext)
        {
            _context = appDbContext;
        }

        //public async Task<Materials> BuildMaterials(Materials material)
        //{
        //    material.Class = await genCtrl.GetClassId(material.ClassId);
            
        //    return material;
        //}

        //public async Task<Materials[]> BuildMaterials(Materials[] materials)
        //{
        //    foreach (var material in materials){
        //        Materials MaterialsTemp = await BuildMaterials(material);
        //        material.Class = MaterialsTemp.Class;
                
        //    }
        //    return materials;
        //}

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Materials>> Create(Materials material)
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
        [Route("")]
        public async Task<ActionResult<IEnumerable<Materials>>> List()
        {
            try
            {
                var materials = await _context.Materials.ToListAsync();
                // materials = (await BuildMaterials(materials.ToArray())).ToList();

                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Route("")]
        public async Task<ActionResult<Materials>> Find(int id)
        {
            try
            {
                var material = await _context.Materials.FindAsync(id);

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

        [HttpPatch("{id}")]
        [Route("")]
        public async Task<IActionResult> Update(int id, Materials updatedMaterial)
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
        [Route("")]
        public async Task<IActionResult> Delete(int id)
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