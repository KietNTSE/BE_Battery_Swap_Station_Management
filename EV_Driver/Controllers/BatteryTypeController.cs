using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatteryTypeController(IBatteryTypeService service) : ControllerBase
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
        public async Task<IActionResult> Add([FromBody] BatteryTypeRequest request)
        {
            await service.AddAsync(request);
            return Ok(new { message = "BatteryType created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] BatteryTypeRequest request)
        {
            request.BatteryTypeId = id;
            await service.UpdateAsync(request);
            return Ok(new { message = "BatteryType updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await service.DeleteAsync(id);
            return Ok(new { message = "BatteryType deleted successfully" });
        }
    }
}

