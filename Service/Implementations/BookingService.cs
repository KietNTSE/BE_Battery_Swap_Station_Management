using BusinessObject;
using Service.Interfaces;

namespace Service.Implementations;

public class BookingService(ApplicationDbContext context) : IBookingService
{
}