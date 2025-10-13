using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController(IReservationService service) : ControllerBase
    {
        // Lấy danh sách reservation có phân trang
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await service.GetAllReservationsAsync(page, pageSize, search);
            return Ok(result);
        }

        // Lấy reservation theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        // Lấy reservation mới nhất theo station inventory id
        [HttpGet("station-inventory/{stationInventoryId}")]
        public async Task<IActionResult> GetByStationInventory(string stationInventoryId)
        {
            try
            {
                var result = await service.GetByStationInventoryAsync(stationInventoryId);
                return Ok(result);
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                return NotFound(new { error = ex.ErrorMessage });
            }
        }

        // Lấy toàn bộ chi tiết reservation (không phân trang)
        [HttpGet("details/all")]
        public async Task<IActionResult> GetAllDetails()
        {
            var result = await service.GetReservationDetailAsync();
            return Ok(result);
        }

        // Thêm reservation
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReservationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await service.AddAsync(request);
                return Ok(new { message = "Reservation created successfully" });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                return BadRequest(new { error = ex.ErrorMessage });
            }
        }

        // Sửa reservation
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ReservationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.ReservationId = id;
            try
            {
                await service.UpdateAsync(request);
                return Ok(new { message = "Reservation updated successfully" });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(new { error = ex.ErrorMessage });
                return BadRequest(new { error = ex.ErrorMessage });
            }
        }

        // Xoá reservation
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Ok(new { message = "Reservation deleted successfully" });
            }
            catch (Service.Exceptions.ValidationException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return NotFound(new { error = ex.ErrorMessage });
                return BadRequest(new { error = ex.ErrorMessage });
            }
        }
    }
}