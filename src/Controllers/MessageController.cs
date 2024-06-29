using Microsoft.AspNetCore.Mvc;

namespace AreaDoAluno.Controllers
{
    [Route("message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly DataContext _appDbContext;

        public MessageController(DataContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult<Message>> AddMessage(Message message)
        {
            try
            {
                _appDbContext.Messages.Add(message);
                await _appDbContext.SaveChangesAsync();
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
                var messages = await _appDbContext.Messages.ToListAsync();
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
                var message = await _appDbContext.Messages.FindAsync(id);

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

                var existingMessage = await _appDbContext.Messages.FindAsync(id);

                if (existingMessage == null)
                {
                    return NotFound("Message not found");
                }

                existingMessage.Content = updatedMessage.Content; // Assuming 'Content' is a property to update
                await _appDbContext.SaveChangesAsync();

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
                var message = await _appDbContext.Messages.FindAsync(id);

                if (message == null)
                {
                    return NotFound("Message not found");
                }

                _appDbContext.Messages.Remove(message);
                await _appDbContext.SaveChangesAsync();

                return Ok("Message deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}