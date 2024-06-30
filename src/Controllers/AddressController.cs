using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    public class AddressController : Controller
    {
        private readonly DataContext _context;

        public AddressController(DataContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("get_id")]
        public async Task<ActionResult<Adress>> GetId(int id)
        {
            try{
                var adress = await _context.Adress.FirstOrDefaultAsync(a => a.Id == id);

                if (adress == null)
                    return NotFound();
                
                return adress;
            } catch{
                return StatusCode(500);
            }

        }

        
    }
}
