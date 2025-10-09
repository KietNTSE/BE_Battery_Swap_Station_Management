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
        public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId) =>
            Ok(await service.GetByUserAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubscriptionRequest request)
        {
            await service.AddAsync(request);
            return Ok(new { message = "Subscription created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SubscriptionRequest request)
        {
            request.SubscriptionId = id;
            await service.UpdateAsync(request);
            return Ok(new { message = "Subscription updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await service.DeleteAsync(id);
            return Ok(new { message = "Subscription deleted successfully" });
        }
    }
}

