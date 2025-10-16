using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatteryController(IBatteryService batteryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<BatteryResponse>>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var battery = await batteryService.GetAllBatteriesAsync(page, pageSize, search);
            return Ok(new ResponseObject<List<BatteryResponse>>
            {
                Message = "Get batteries successfully",
                Code = "200",
                Success = true
            }.UnwrapPagination(battery));
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<BatteryResponse>>> GetById(string id)
        {
            var battery = await batteryService.GetByBatteryAsync(id);
            return Ok(new ResponseObject<BatteryResponse>
            {
                Message = "Get batteries successfully",
                Code = "200",
                Success = true,
                Content = battery
            });
        }

        // Lấy pin theo SerialNo 
        [HttpGet("serial/{serialNo:int}")]
        public async Task<ActionResult<ResponseObject<BatteryResponse>>> GetBySerial(int serialNo)
        {
            var battery = await batteryService.GetBySerialAsync(serialNo);
            return Ok(new ResponseObject<BatteryResponse>
            {
                Message = "Get batteries by serial number successfully",
                Code = "200",
                Success = true,
                Content = battery
            });
        }
        
        [HttpGet("station/{stationId}")]
        public async Task<ActionResult<ResponseObject<BatteryResponse>>> GetByStation(string stationId)
        {
             var battery = await batteryService.GetByStationAsync(stationId);
             return Ok(new ResponseObject<BatteryResponse>
             {
                 Message = "Get Station ID successfully",
                 Code = "200",
                 Success = true,
                 Content = battery
             });
        }

        // Lấy pin sẵn sàng (Available), có thể lọc theo trạm
        [HttpGet("available")]
        public async Task<ActionResult<ResponseObject<BatteryResponse>>> GetAvailable([FromQuery] string stationId)
        {
             var battery = await batteryService.GetAvailableAsync(stationId);
             return Ok(new ResponseObject<BatteryResponse>
             {
                 Message = "Get Available Battery by Station successfully",
                 Code = "200",
                 Success = true,
                 Content = battery
             });
        }

        // Thêm pin mới
        [HttpPost]
        public async Task<ActionResult<ResponseObject<object>>> Add([FromBody] BatteryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await batteryService.AddAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Battery created successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        // Cập nhật pin
        [HttpPut]
        public async Task<ActionResult<ResponseObject<object>>> Update([FromBody] BatteryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

             await batteryService.UpdateAsync(request);
             return Ok(new ResponseObject<object>
             {
                 Message = "Battery updated successfully",
                 Code = "200",
                 Success = true,
                 Content = null
             });
        }

        // Xóa pin
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Delete(string id)
        {
             await batteryService.DeleteAsync(id);
             return Ok(new ResponseObject<object>
             {
                 Message = "Battery deleted successfully",
                 Code = "200",
                 Success = true,
                 Content = null
             });
        }
    }
}