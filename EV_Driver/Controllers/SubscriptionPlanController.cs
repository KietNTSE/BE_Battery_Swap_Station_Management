using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPlanController(ISubscriptionPlanService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SubscriptionPlanRequest request)
        {
            await service.AddAsync(request);
            return Ok(new { message = "Subscription plan created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SubscriptionPlanRequest request)
        {
            request.PlanId = id;
            await service.UpdateAsync(request);
            return Ok(new { message = "Subscription plan updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await service.DeleteAsync(id);
            return Ok(new { message = "Subscription plan deleted successfully" });
        }
    }
}
