using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly DataContext _context;

        public ClassController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List()
        {
            try{
                var classes = await _context.Class.ToListAsync();

                return Ok(classes);
            } catch {
                return StatusCode (400);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Class>> Find(int id)
        {
            try{
                var _class = await _context.Class.FirstOrDefaultAsync((_class) => _class.Id == id);

                if(_class is null)
                    return NotFound();

                return Ok(_class);
            }catch{
                return StatusCode (500);
            }
        }

        [HttpPost]
        [Route("")] 
        public ActionResult<Class> Create(Class _class) 
        {
            _context.Add(_class);
            _context.SaveChanges();
            return Created("", _class);
        }

        [HttpPatch]
        [Route("")]
        public async Task<ActionResult<Class>> Update(Class newClass) 
        {
            try {
                var _class = await _context.Class.FindAsync(newClass.Id);

                if (_class == null)
                {
                    return NotFound("Class not found");
                }

                foreach (var property in newClass.GetType().GetProperties())
                {
                    var newValue = newClass.GetType().GetProperty(property.Name)?.GetValue(newClass);
                    property.SetValue(_class, newValue);
                }

                await _context.SaveChangesAsync();

                return Ok(newClass);
            } catch {
                return StatusCode(400);
            }
        }

        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delete(int id) 
        {
            try {
                var _class = await _context.Class.FindAsync(id);
                
                if (_class == null)
                {
                    return NotFound("Class not found");
                }

                _context.Class.Remove(_class);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            } catch {
                return StatusCode(500);
            }
        }

        
    }
}
