using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ResponseObject<object>>> CreateBooking(CreateBookingRequest request)
    {
        await bookingService.CreateBooking(request);
        return Ok(new ResponseObject<string>
        {
            Message = "Booking created successfully",
            Code = "200",
            Success = true
        });
    }
}