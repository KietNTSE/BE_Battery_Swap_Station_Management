using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatteryController(IBatteryService batteryService) : ControllerBase
    {
        // Lấy danh sách toàn bộ pin (có phân trang)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var result = await batteryService.GetAllBatteriesAsync(page, pageSize, search);
            return Ok(result);
        }

        // Lấy pin theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var battery = await batteryService.GetByBatteryAsync(id);
            if (battery == null)
                return NotFound(new { message = "Battery not found." });

            return Ok(battery);
        }

        // Lấy pin theo SerialNo 
        [HttpGet("serial/{serialNo:int}")]
        public async Task<IActionResult> GetBySerial(int serialNo)
        {
            var battery = await batteryService.GetBySerialAsync(serialNo);
            if (battery == null)
                return NotFound(new { message = "Battery not found." });

            return Ok(battery);
        }

        // Lấy pin mới nhất theo trạm
        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetByStation(string stationId)
        {
            try
            {
                var battery = await batteryService.GetByStationAsync(stationId);
                return Ok(battery);
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // Lấy pin sẵn sàng (Available), có thể lọc theo trạm
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable([FromQuery] string? stationId)
        {
            try
            {
                var battery = await batteryService.GetAvailableAsync(stationId);
                return Ok(battery);
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        // Thêm pin mới
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BatteryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await batteryService.AddAsync(request);
                return Ok(new { message = "Battery added successfully." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Cập nhật pin
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] BatteryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await batteryService.UpdateAsync(request);
                return Ok(new { message = "Battery updated successfully." });
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Xóa pin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await batteryService.DeleteAsync(id);
                return Ok(new { message = "Battery deleted successfully." });
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}