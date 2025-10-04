using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController(IVehicleService vehicleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ResponseObject<List<VehicleResponse>>>> GetUserVehicle(int page, int pageSize,
        string? search)
    {
        var vehicle = await vehicleService.GetAllVehiclesAsync(page, pageSize, search);
        return Ok(new ResponseObject<List<VehicleResponse>>
        {
            Message = "Get vehicle successfully",
            Code = "200",
            Success = true
        }.UnwrapPagination(vehicle));
    }

    [HttpPost]
    public async Task<ActionResult<ResponseObject<VehicleResponse>>> CreateUserVehicle(
        [FromBody] VehicleRequest request)
    {
        var vehicle = await vehicleService.CreateVehicleAsync(request);
        return Ok(new ResponseObject<VehicleResponse>
        {
            Content = vehicle,
            Message = "Create vehicle successfully",
            Code = "200",
            Success = true
        });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseObject<VehicleResponse>>> UpdateUserVehicle(string id,
        [FromBody] VehicleRequest request)
    {
        var vehicle = await vehicleService.UpdateVehicleAsync(id, request);
        return Ok(new ResponseObject<VehicleResponse>
        {
            Content = vehicle,
            Message = "Update vehicle successfully",
            Code = "200",
            Success = true
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseObject<VehicleResponse>>> GetUserVehicle(string id)
    {
        var vehicle = await vehicleService.GetVehicleAsync(id);
        return Ok(new ResponseObject<VehicleResponse>
        {
            Content = vehicle,
            Message = "Get vehicle successfully",
            Code = "200",
            Success = true
        });
    }
}