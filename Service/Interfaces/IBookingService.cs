using BusinessObject.DTOs;

namespace Service.Interfaces;

public interface IBookingService
{
    Task<BookingResponse> GetBookingAsync(string bookingId);
    Task CreateBooking(CreateBookingRequest request);
    Task<PaginationWrapper<List<BookingResponse>, BookingResponse>> GetAllBookingAsync(int page,
        int pageSize, string? search);

    Task<PaginationWrapper<List<BookingResponse>, BookingResponse>> GetAllMyBookingAsync(int page, int pageSize,
        string? search);
}