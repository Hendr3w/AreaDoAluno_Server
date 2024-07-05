using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly DataContext _context;

        public PersonController(DataContext appDbContext) {
            _context = appDbContext;
        }

        [HttpPost]
        [Route("")] 
        public async Task<ActionResult<Person>> Create(Person person) 
        {
            _context.Add(person);
            await _context.SaveChangesAsync();
            return Created("", person);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<Person>> Signin(SignInData signinData) {
            try {
                var person = await _context.Person.FirstOrDefaultAsync(person => person.Email == signinData.Email);

                if (person == null) {
                    return NotFound("E-mail não encontrado");
                }

                if (person.Password != signinData.Password) {
                    return Unauthorized("Senha incorreta");
                }

                return Ok(person);

            } catch {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Person>> Find(int id) 
        {
            try {
                var person = await _context.Person.FirstOrDefaultAsync(p => p.Id == id);

                if (person == null) {
                    return NotFound("User not found");
                }
                
                return Ok(person);
            } catch {
                return StatusCode(500);
            }
        }
    }
}