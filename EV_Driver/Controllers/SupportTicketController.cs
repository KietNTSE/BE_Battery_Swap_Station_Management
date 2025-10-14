using System.ComponentModel.DataAnnotations;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportTicketController(ISupportTicketService supportTicketService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<SupportTicketResponse>>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var tickets = await supportTicketService.GetAllSupportTicketAsync(page, pageSize, search);
            return Ok(new ResponseObject<List<SupportTicketResponse>>
            {
                Message = "Get support tickets successfully",
                Code = "200",
                Success = true
            }.UnwrapPagination(tickets));
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseObject<List<SupportTicketResponse>>>> GetSupportTicketDetails()
        {
            var tickets = await supportTicketService.GetSupportTicketDetailAsync();
            return Ok(new ResponseObject<List<SupportTicketResponse>>
            {
                Message = "Get support ticket details successfully",
                Code = "200",
                Success = true,
                Content = tickets
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<SupportTicketResponse>>> GetById(string id)
        {
            var ticket = await supportTicketService.GetBySupportTicketAsync(id);
            if (ticket == null)
            {
                return Ok(new ResponseObject<SupportTicketResponse>
                {
                    Message = "Support ticket not found",
                    Code = "404",
                    Success = false,
                    Content = null
                });
            }

            return Ok(new ResponseObject<SupportTicketResponse>
            {
                Message = "Get support ticket successfully",
                Code = "200",
                Success = true,
                Content = ticket
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<object>>> Create([FromBody] SupportTicketRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await supportTicketService.AddAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Support ticket created successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Update(string id, [FromBody] SupportTicketRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.TicketId = id;
            await supportTicketService.UpdateAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Support ticket updated successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Delete(string id)
        {
            await supportTicketService.DeleteAsync(id);
            return Ok(new ResponseObject<object>
            {
                Message = "Support ticket deleted successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }
    }
}