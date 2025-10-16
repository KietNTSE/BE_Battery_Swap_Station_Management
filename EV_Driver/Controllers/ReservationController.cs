using System.Net;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController(IReservationService service) : ControllerBase
    {
        // Lấy danh sách reservation có phân trang
        [HttpGet]
        public async Task<ActionResult<ResponseObject<List<ReservationResponse>>>> GetAll([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? search)
        {
            var reservation = await service.GetAllReservationsAsync(page, pageSize, search);
            return Ok(new ResponseObject<List<ReservationResponse>> {
                Message = "Get reservation successfully",
                Code = "200",
                Success = true
            }.UnwrapPagination(reservation));
        }

        // Lấy reservation theo id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseObject<ReservationResponse>>> GetById(string id)
        {
            var result = await service.GetByIdAsync(id);
            return Ok(new ResponseObject<ReservationResponse> {
                Message = "Get reservation by ID",
                Code = "200",
                Success = true
            });
        }

        // Lấy reservation mới nhất theo station inventory id
        [HttpGet("station-inventory/{stationInventoryId}")]
        public async Task<ActionResult<ResponseObject<ReservationResponse>>> GetByStationInventory(string stationInventoryId)
        {
            try
            {
                var result = await service.GetByStationInventoryAsync(stationInventoryId);
                return Ok(new ResponseObject<ReservationResponse> {
                    Content = result,
                    Message = "Get new reservation by station inventory ID",
                    Code = "200",
                    Success = true
                });
            }
            catch (ValidationException ex)
            {
                return NotFound(new { error = ex.ErrorMessage });
            }
        }

        // Lấy toàn bộ chi tiết reservation (không phân trang)
        [HttpGet("details/all")]
        public async Task<ActionResult<ResponseObject<ReservationResponse>>> GetAllDetails()
        {
            var result = await service.GetReservationDetailAsync();
            return Ok(new ResponseObject<List<ReservationResponse>> {
                Content = result,
                Message = "Create vehicle successfully",
                Code = "200",
                Success = true
            });
        }

        // Thêm reservation
        [HttpPost]
        public async Task<ActionResult<ResponseObject<object>>> Add([FromBody] ReservationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await service.AddAsync(request);
                return Ok(new ResponseObject<object>{
                    Content = null,
                    Message = "add reservation  successfully",
                    Code = "200",
                    Success = true
                });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { error = ex.ErrorMessage });
            }
        }

        // Sửa reservation
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Update(string id, [FromBody] ReservationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.ReservationId = id;
            try
            {
                await service.UpdateAsync(request);
                return Ok(new ResponseObject<object>{
                    Content = null,
                    Message = "update reservation successfully",
                    Code = "200",
                    Success = true
                });
            }
            catch (ValidationException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(new { error = ex.ErrorMessage });
                return BadRequest(new { error = ex.ErrorMessage });
            }
        }

        // Xoá reservation
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseObject<object>>> Delete(string id)
        {
            try
            {
                await service.DeleteAsync(id);
                return Ok(new ResponseObject<object>{
                    Content = null,
                    Message = "Delete reservation successfully",
                    Code = "200",
                    Success = true
                });
            }
            catch (ValidationException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(new { error = ex.ErrorMessage });
                return BadRequest(new { error = ex.ErrorMessage });
            }
        }
    }
}