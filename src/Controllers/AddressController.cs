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



        //[HttpGet]
        //[Route("{id}")]
        //public async Task<ActionResult<Adress>> GetId(int id)
        //{
        //    try{
        //        var adress = await _context.Adress.FirstOrDefaultAsync(a => a.Id == id);

        //        if (adress == null)
        //            return NotFound();

        //        return adress;
        //    } catch{
        //        return StatusCode(500);
        //    }
        //}


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Address>> Create(Address address)
        {
            try
            {
                _context.Add(address);
                await _context.SaveChangesAsync();
                return Ok(address);
            } catch {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Address>> Find(int id)
        {
            try
            {
                var address = await _context.Address.FirstOrDefaultAsync(p => p.Id == id);

                if (address == null)
                {
                    return NotFound("Student not found");
                }

                return Ok(address);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
