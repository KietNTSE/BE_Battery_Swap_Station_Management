using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
     
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await paymentService.GetAllAsync();
            return Ok(new { Success = true, Data = payments });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var payment = await paymentService.GetByIdAsync(id);
            if (payment == null)
                return NotFound(new { Success = false, Message = "Payment not found." });

            return Ok(new { Success = true, Data = payment });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            var payments = await paymentService.GetByUserAsync(userId);
            return Ok(new { Success = true, Data = payments });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentRequest request)
        {
            await paymentService.AddAsync(request);
            return Ok(new { Success = true, Message = "Payment created successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PaymentRequest request)
        {
            await paymentService.UpdateAsync(request);
            return Ok(new { Success = true, Message = "Payment updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await paymentService.DeleteAsync(id);
            return Ok(new { Success = true, Message = "Payment deleted successfully." });
        }
    }
}
