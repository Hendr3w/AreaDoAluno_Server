using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext appDbContext) {
            _context = appDbContext;
        }

        [HttpPost]
        [Route("")] 
        public async Task<ActionResult<User>> Create(User User) 
        {
            _context.Add(User);
            await _context.SaveChangesAsync();
            return Created("", User);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<User>> Signin(SignInData signinData) {
            try {
                var User = await _context.User.FirstOrDefaultAsync(User => User.Email == signinData.Email);

                if (User == null) {
                    return NotFound("E-mail não encontrado");
                }

                if (User.Password != signinData.Password) {
                    return Unauthorized("Senha incorreta");
                }

                return Ok(User);

            } catch {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<User>> List() 
        {
            try {
                var users = await _context.User.ToListAsync();

                return Ok(users);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<User>> Find(int id) 
        {
            try {
                var User = await _context.User.FirstOrDefaultAsync(p => p.Id == id);

                if (User == null) {
                    return NotFound("User not found");
                }
                
                return Ok(User);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<User>> Delete(int id) 
        {
            try {
                var user = await _context.User.FindAsync(id);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                _context.User.Remove(user);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            } catch {
                return StatusCode(500);
            }
        }
    }
}