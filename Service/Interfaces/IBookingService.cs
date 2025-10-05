using BusinessObject.DTOs;

namespace Service.Interfaces;

public interface IBookingService
{
    Task CreateBooking(CreateBookingRequest request);
}