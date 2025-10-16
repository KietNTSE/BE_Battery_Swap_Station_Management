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

    [HttpGet]
    public async Task<ActionResult<ResponseObject<List<BookingResponse>>>> GetAllBookingAsync([FromQuery] int page, [FromQuery] int size, [FromQuery] string? search)
    {
        var result = await bookingService.GetAllBookingAsync(page, size, search);
        return Ok(new ResponseObject<List<BookingResponse>>
        {
            Message = "Get booking successfully",
            Code = "200",
            Success = true
        }.UnwrapPagination(result));
    }
    
    [HttpGet("me")]
    public async Task<ActionResult<ResponseObject<List<BookingResponse>>>> GetAllMyBookingAsync([FromQuery] int page, [FromQuery] int size, [FromQuery] string? search)
    {
        var result = await bookingService.GetAllMyBookingAsync(page, size, search);
        return Ok(new ResponseObject<List<BookingResponse>>
        {
            Message = "Get booking successfully",
            Code = "200",
            Success = true
        }.UnwrapPagination(result));
    }

    [HttpGet("{bookingId}")]
    public async Task<ActionResult<ResponseObject<BookingResponse>>> GetBookingById(string bookingId)
    {
        var result = await bookingService.GetBookingAsync(bookingId);
        return Ok(new ResponseObject<BookingResponse>
        {
            Content = result,
            Message = "Get booking successfully",
            Code = "200",
            Success = true
        });
    }
}