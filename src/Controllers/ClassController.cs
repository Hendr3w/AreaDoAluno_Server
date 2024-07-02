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

        
        GeneralController genCtrl = new();

        [HttpGet]
        public async Task<Class> BuildClass(Class _class)
        {
            _class.Professor = await genCtrl.GetProfessorId(_class.ProfessorId);
            _class.Discipline = await genCtrl.GetDisciplineId(_class.DisciplineId);
            
            return _class;
        }

        [HttpGet]
        public async Task<Class[]> BuildClasses(Class[] _classes)
        {
            foreach (var _class in _classes){
                Class classTemp = await BuildClass(_class);
                _class.Professor = classTemp.Professor;
                _class.Discipline = classTemp.Discipline;
            }
            return _classes;
        }


        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            try{
                var classes = await _context.Class.ToListAsync();
                classes = (await BuildClasses(classes.ToArray())).ToList();

                return Ok(classes);
            } catch {
                return StatusCode (400);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Class>> GetId(int id)
        {
            try{
                var _class = await _context.Class.FirstOrDefaultAsync((_class) => _class.Id == id);

                if(_class is null)
                    return NotFound();

                _class = await BuildClass(_class);

                return Ok(_class);
            }catch{
                return StatusCode (500);
            }
        }

        [HttpPost]
        public ActionResult<Class> AddClass(Class _class) 
        {
            _context.Add(_class);
            _context.SaveChanges();
            return Created("", _class);
        }

        [HttpPut]
        public async Task<ActionResult<Class>> UpdateClass(Class newClass) 
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

        [HttpDelete("{id}")]
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

                return Ok("Class deleted");
            } catch {
                return StatusCode(500);
            }
        }

        
    }
}
