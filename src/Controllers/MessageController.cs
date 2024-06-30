using AreaDoAluno.Data;
using AreaDoAluno.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AreaDoAluno.Controllers
{
    [Route("message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _context;
        GeneralController genCtrl = new();

        public MessageController(DataContext appDbContext)
        {
            _context = appDbContext;
        }

         public async Task<Message> BuildMessage(Message _Message)
        {
            _Message.Class = await genCtrl.GetClassId(_Message.ClassId);
    
            
            return _Message;
        }

        public async Task<Message[]> BuildMessage(Message[] _Messages)
        {
            foreach (var _Message in _Messages){
                Message MessageTemp = await BuildMessage(_Message);
                _Message.Class = MessageTemp.Class;
            }
            return _Messages;
        }


        [HttpPost]
        [Route("cadastrar")]
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
        [Route("all")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, Message updatedMessage)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
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

                return Ok("Message deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}