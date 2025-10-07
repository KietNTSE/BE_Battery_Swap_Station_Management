using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationInventoryController(IStationInventoryService stationInventoryService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await stationInventoryService.GetAllAsync();
            return Ok(new { Success = true, Message = "Fetched all station inventories.", Data = inventories });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var inventory = await stationInventoryService.GetByIdAsync(id);
            if (inventory == null)
                return NotFound(new { Success = false, Message = "Station inventory not found." });

            return Ok(new { Success = true, Data = inventory });
        }

        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetByStation(string stationId)
        {
            var inventory = await stationInventoryService.GetByStationIdAsync(stationId);
            if (inventory == null)
                return NotFound(new { Success = false, Message = "No inventory found for this station." });

            return Ok(new { Success = true, Data = inventory });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StationInventoryRequest request)
        {
            await stationInventoryService.AddAsync(request);
            return Ok(new { Success = true, Message = "Station inventory added successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StationInventoryRequest request)
        {
            await stationInventoryService.UpdateAsync(request);
            return Ok(new { Success = true, Message = "Station inventory updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await stationInventoryService.DeleteAsync(id);
            return Ok(new { Success = true, Message = "Station inventory deleted successfully." });
        }
    }
}
