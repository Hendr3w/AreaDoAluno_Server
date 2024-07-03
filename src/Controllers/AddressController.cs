using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly DataContext _context;

        public AddressController(DataContext context)
        {
            _context = context;
        }



        [HttpGet("{Id}")]
        public async Task<ActionResult<Adress>> GetId(int Id)
        {
            try{
                var adress = await _context.Adress.FirstOrDefaultAsync(a => a.Id == Id);

                if (adress == null)
                    return NotFound();
                
                return adress;
            } catch{
                return StatusCode(500);
            }

        }

        
    }
}
