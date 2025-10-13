using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController(ISubscriptionService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await service.GetAllSubscriptionAsync(page, pageSize, search);
            return Ok(new { Success = true, Message = "Fetched all subscriptions.", Data = result });
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetSubscriptionDetails()
        {
            var subscriptions = await service.GetSubscriptionDetailAsync();
            return Ok(new { Success = true, Message = "Fetched subscription details.", Data = subscriptions });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetBySubscriptionAsync(id);
            if (result == null)
                return NotFound(new { Success = false, Message = "Subscription not found." });

            return Ok(new { Success = true, Data = result });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            try
            {
                var result = await service.GetByUserAsync(userId);
                return Ok(new { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubscriptionRequest request)
        {
            try
            {
                await service.AddAsync(request);
                return Ok(new { Success = true, Message = "Subscription created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SubscriptionRequest request)
        {
            try
            {
                request.SubscriptionId = id;
                await service.UpdateAsync(request);
                return Ok(new { Success = true, Message = "Subscription updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Ok(new { Success = true, Message = "Subscription deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }
    }
}