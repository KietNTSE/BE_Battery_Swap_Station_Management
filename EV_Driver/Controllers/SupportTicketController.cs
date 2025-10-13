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
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var tickets = await supportTicketService.GetAllSupportTicketAsync(page, pageSize, search);
            return Ok(new
            {
                Success = true,
                Message = "Fetched all support tickets successfully.",
                Data = tickets
            });
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetSupportTicketDetails()
        {
            var tickets = await supportTicketService.GetSupportTicketDetailAsync();
            return Ok(new
            {
                Success = true,
                Message = "Fetched support ticket details successfully.",
                Data = tickets
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var ticket = await supportTicketService.GetBySupportTicketAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupportTicketRequest request)
        {
            try
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SupportTicketRequest request)
        {
            try
            {
                request.TicketId = id;
                await supportTicketService.UpdateAsync(request);
                return Ok(new
                {
                    Success = true,
                    Message = "Support ticket updated successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await supportTicketService.DeleteAsync(id);
                return Ok(new
                {
                    Success = true,
                    Message = "Support ticket deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}