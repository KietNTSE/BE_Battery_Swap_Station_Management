using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatteryController(IBatteryService batteryService) : ControllerBase
    {

        //Lấy danh sách toàn bộ pin
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var batteries = await batteryService.GetAllAsync();
            return Ok(batteries);
        }

        //Lấy pin theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var battery = await batteryService.GetByIdAsync(id);
            if (battery == null)
                return NotFound(new { message = "Battery not found." });

            return Ok(battery);
        }

        //Lấy pin theo SerialNo 
        [HttpGet("serial/{serialNo:int}")]
        public async Task<IActionResult> GetBySerial(int serialNo)
        {
            var battery = await batteryService.GetBySerialAsync(serialNo);
            if (battery == null)
                return NotFound(new { message = "Battery not found." });

            return Ok(battery);
        }

        //Lấy pin theo trạm
        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetByStation(string stationId)
        {
            var batteries = await batteryService.GetByStationAsync(stationId);
            return Ok(batteries);
        }

        //Lấy các pin đang sẵn sàng (Available)
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable([FromQuery] string? stationId)
        {
            var available = await batteryService.GetAvailableAsync(stationId);
            return Ok(available);
        }

        //Thêm pin mới
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
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //Cập nhật pin
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
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        //Xóa pin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await batteryService.DeleteAsync(id);
                return Ok(new { message = "Battery deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
