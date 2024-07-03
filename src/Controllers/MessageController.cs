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
        public async Task<ActionResult<Message>> AddMessage(Message message)
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
        public async Task<ActionResult<IEnumerable<Message>>> GetAllMessages()
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

        [HttpGet("{Id}")]
        public async Task<ActionResult<Message>> GetMessageById(int Id)
        {
            try
            {
                var message = await _context.Message.FindAsync(Id);

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

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateMessage(int Id, Message updatedMessage)
        {
            try
            {
                if (Id != updatedMessage.Id)
                {
                    return BadRequest("Invalid message ID");
                }

                var existingMessage = await _context.Message.FindAsync(Id);

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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMessage(int Id)
        {
            try
            {
                var message = await _context.Message.FindAsync(Id);

                if (message == null)
                {
                    return NotFound("Message not found");
                }

                _context.Message.Remove(message);
                await _context.SaveChangesAsync();

                return Ok("Message deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}