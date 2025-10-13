using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationBatterySlotController(IStationBatterySlotService service) : ControllerBase
    {
        // GET: api/StationBatterySlot?page=1&pageSize=10&search=...
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await service.GetAllStationSlotAsync(page, pageSize, search);
            return Ok(new { Success = true, Message = "Fetched all station battery slots.", Data = result });
        }

        // GET: api/StationBatterySlot/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var slot = await service.GetByIdAsync(id);
            if (slot == null)
                return NotFound(new { Success = false, Message = "Station battery slot not found." });
            return Ok(new { Success = true, Data = slot });
        }

        // GET: api/StationBatterySlot/station/{stationId}
        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetByStation(string stationId)
        {
            try
            {
                var slot = await service.GetByStationAsync(stationId);
                return Ok(new { Success = true, Data = slot });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                return NotFound(new { Success = false, Message = ex.ErrorMessage });
            }
        }

        // GET: api/StationBatterySlot/details/all
        [HttpGet("details/all")]
        public async Task<IActionResult> GetAllDetails()
        {
            var result = await service.GetStationBatterySlotDetailAsync();
            return Ok(new { Success = true, Data = result });
        }

        // POST: api/StationBatterySlot
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StationBatterySlotRequest request)
        {
            try
            {
                await service.AddAsync(request);
                return Ok(new { Success = true, Message = "Station battery slot added successfully." });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                return BadRequest(new { Success = false, Message = ex.ErrorMessage });
            }
        }

        // PUT: api/StationBatterySlot
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StationBatterySlotRequest request)
        {
            try
            {
                await service.UpdateAsync(request);
                return Ok(new { Success = true, Message = "Station battery slot updated successfully." });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(new { Success = false, Message = ex.ErrorMessage });
                return BadRequest(new { Success = false, Message = ex.ErrorMessage });
            }
        }

        // DELETE: api/StationBatterySlot/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Ok(new { Success = true, Message = "Station battery slot deleted successfully." });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(new { Success = false, Message = ex.ErrorMessage });
                return BadRequest(new { Success = false, Message = ex.ErrorMessage });
            }
        }
    }
}