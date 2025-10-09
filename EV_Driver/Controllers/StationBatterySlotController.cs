using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationBatterySlotController(IStationBatterySlotService service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetByStation(string stationId) =>
            Ok(await service.GetByStationAsync(stationId));

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StationBatterySlotRequest request)
        {
            await service.AddAsync(request);
            return Ok(new { message = "Slot created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StationBatterySlotRequest request)
        {
            request.StationSlotId = id;
            await service.UpdateAsync(request);
            return Ok(new { message = "Slot updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await service.DeleteAsync(id);
            return Ok(new { message = "Slot deleted successfully" });
        }
    }
}
