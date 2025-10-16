using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController(ISubscriptionService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<SubscriptionResponse>>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await service.GetAllSubscriptionAsync(page, pageSize, search);
            return Ok(new ResponseObject<List<SubscriptionResponse>>
            {
                Message = "Get subscriptions successfully",
                Code = "200",
                Success = true
            }.UnwrapPagination(result));
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseObject<List<SubscriptionResponse>>>> GetSubscriptionDetails()
        {
            var subscriptions = await service.GetSubscriptionDetailAsync();
            return Ok(new ResponseObject<List<SubscriptionResponse>>
            {
                Message = "Get subscription details successfully",
                Code = "200",
                Success = true,
                Content = subscriptions
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<SubscriptionResponse>>> GetById(string id)
        {
            var result = await service.GetBySubscriptionAsync(id);
            if (result == null)
            {
                return Ok(new ResponseObject<SubscriptionResponse>
                {
                    Message = "Subscription not found",
                    Code = "404",
                    Success = false,
                    Content = null
                });
            }

            return Ok(new ResponseObject<SubscriptionResponse>
            {
                Message = "Get subscription successfully",
                Code = "200",
                Success = true,
                Content = result
            });
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ResponseObject<SubscriptionResponse>>> GetByUser(string userId)
        {
            var result = await service.GetByUserAsync(userId);
            return Ok(new ResponseObject<SubscriptionResponse>
            {
                Message = "Get subscription by user successfully",
                Code = "200",
                Success = true,
                Content = result
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<object>>> Add([FromBody] SubscriptionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await service.AddAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Subscription created successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Update(string id, [FromBody] SubscriptionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.SubscriptionId = id;
            await service.UpdateAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Subscription updated successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Delete(string id)
        {
            await service.DeleteAsync(id);
            return Ok(new ResponseObject<object>
            {
                Message = "Subscription deleted successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }
    }
}