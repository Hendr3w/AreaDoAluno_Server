using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _context;

        public MessageController(DataContext appDbContext)
        {
            _context = appDbContext;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Message>> Create(Message message)
        {
            try
            {
                _context.Message.Add(message);
                await _context.SaveChangesAsync();
                return Created("", message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Message>>> List()
        {
            try
            {
                var messages = await _context.Message.ToListAsync();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Message>> Find(int id)
        {
            try
            {
                var message = await _context.Message.FindAsync(id);

                if (message == null)
                {
                    return NotFound("Message not found");
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, Message updatedMessage)
        {
            try
            {
                if (id != updatedMessage.Id)
                {
                    return BadRequest("Invalid message ID");
                }

                var existingMessage = await _context.Message.FindAsync(id);

                if (existingMessage == null)
                {
                    return NotFound("Message not found");
                }

                existingMessage.Body = updatedMessage.Body;
                await _context.SaveChangesAsync();

                return Ok(existingMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var message = await _context.Message.FindAsync(id);

                if (message == null)
                {
                    return NotFound("Message not found");
                }

                _context.Message.Remove(message);
                await _context.SaveChangesAsync();

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}