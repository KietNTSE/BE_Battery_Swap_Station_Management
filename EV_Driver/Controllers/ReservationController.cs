using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController(IReservationService reservationService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await reservationService.GetAllAsync();
            return Ok(new { Success = true, Message = "Fetched all reservations.", Data = result });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await reservationService.GetByIdAsync(id);
            if (result == null)
                return NotFound(new { Success = false, Message = "Reservation not found." });
            return Ok(new { Success = true, Data = result });
        }

        [HttpGet("stationinventory/{stationInventoryId}")]
        public async Task<IActionResult> GetByStationInventory(string stationInventoryId)
        {
            var result = await reservationService.GetByStationInventoryAsync(stationInventoryId);
            return Ok(new { Success = true, Message = "Fetched reservations by station inventory.", Data = result });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservationRequest request)
        {
            await reservationService.AddAsync(request);
            return Ok(new { Success = true, Message = "Reservation created successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReservationRequest request)
        {
            await reservationService.UpdateAsync(request);
            return Ok(new { Success = true, Message = "Reservation updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await reservationService.DeleteAsync(id);
            return Ok(new { Success = true, Message = "Reservation deleted successfully." });
        }
    }
}
