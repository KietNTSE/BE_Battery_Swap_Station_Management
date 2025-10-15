using System.ComponentModel.DataAnnotations;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StationInventoryController(IStationInventoryService stationInventoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<StationInventoryResponse>>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            var inventories = await stationInventoryService.GetAllStationInventoryAsync(page, pageSize, search);
            return Ok(new ResponseObject<List<StationInventoryResponse>>
            {
                Message = "Get station inventories successfully",
                Code = "200",
                Success = true
            }.UnwrapPagination(inventories));
        }

        [HttpGet("details")]
        public async Task<ActionResult<ResponseObject<List<StationInventoryResponse>>>> GetStationInventoryDetails()
        {
            var inventories = await stationInventoryService.GetStationInventoryDetailAsync();
            return Ok(new ResponseObject<List<StationInventoryResponse>>
            {
                Message = "Get station inventory details successfully",
                Code = "200",
                Success = true,
                Content = inventories
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<StationInventoryResponse>>> GetById(string id)
        {
            var inventory = await stationInventoryService.GetByIdAsync(id);
            if (inventory == null)
            {
                return Ok(new ResponseObject<StationInventoryResponse>
                {
                    Message = "Station inventory not found",
                    Code = "404",
                    Success = false,
                    Content = null
                });
            }

            return Ok(new ResponseObject<StationInventoryResponse>
            {
                Message = "Get station inventory successfully",
                Code = "200",
                Success = true,
                Content = inventory
            });
        }

        [HttpGet("station/{stationId}")]
        public async Task<ActionResult<ResponseObject<StationInventoryResponse>>> GetByStation(string stationId)
        {
            var inventory = await stationInventoryService.GetByStationIdAsync(stationId);
            if (inventory == null)
            {
                return Ok(new ResponseObject<StationInventoryResponse>
                {
                    Message = "No inventory found for this station",
                    Code = "404",
                    Success = false,
                    Content = null
                });
            }

            return Ok(new ResponseObject<StationInventoryResponse>
            {
                Message = "Get station inventory by station successfully",
                Code = "200",
                Success = true,
                Content = inventory
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseObject<object>>> Create([FromBody] StationInventoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await stationInventoryService.AddAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Station inventory created successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpPut]
        public async Task<ActionResult<ResponseObject<object>>> Update([FromBody] StationInventoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await stationInventoryService.UpdateAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Station inventory updated successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Delete(string id)
        {
            await stationInventoryService.DeleteAsync(id);
            return Ok(new ResponseObject<object>
            {
                Message = "Station inventory deleted successfully",
                Code = "200",
                Success = true,
                Content = null
            });
        }
    }
}