using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportTicketController(ISupportTicketService supportTicketService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await supportTicketService.GetAllAsync();
            return Ok(new
            {
                Success = true,
                Message = "Fetched all support tickets successfully.",
                Data = tickets
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var ticket = await supportTicketService.GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound(new
                {
                    Success = false,
                    Message = "Support ticket not found."
                });
            }

            return Ok(new
            {
                Success = true,
                Message = "Fetched support ticket successfully.",
                Data = ticket
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var tickets = await supportTicketService.GetByUserAsync(userId);
            return Ok(new
            {
                Success = true,
                Message = "Fetched user support tickets successfully.",
                Data = tickets
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupportTicketRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid ticket data.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            await supportTicketService.AddAsync(request);
            return Ok(new
            {
                Success = true,
                Message = "Support ticket created successfully."
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] SupportTicketRequest request)
        {
            if (string.IsNullOrEmpty(request.TicketId))
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "TicketId is required for update."
                });
            }

            await supportTicketService.UpdateAsync(request);
            return Ok(new
            {
                Success = true,
                Message = "Support ticket updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await supportTicketService.DeleteAsync(id);
            return Ok(new
            {
                Success = true,
                Message = "Support ticket deleted successfully."
            });
        }
    }
}
