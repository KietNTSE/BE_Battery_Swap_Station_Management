using System.Net;
using BusinessObject;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using Service.Utils;

namespace Service.Implementations;

public class BookingService(ApplicationDbContext context, IHttpContextAccessor accessor) : IBookingService
{
    public async Task CreateBooking(CreateBookingRequest request)
    {
        var userId = JwtUtils.GetUserId(accessor);
        if (string.IsNullOrEmpty(userId))
            throw new ValidationException
            {
                ErrorMessage = "Unauthorized",
                Code = "401",
                StatusCode = HttpStatusCode.Unauthorized
            };

        var vehicle = await context.Vehicles.FirstOrDefaultAsync(v =>
            v.UserId == userId && v.VehicleId == request.VehicleId);
        if (vehicle is null)
            throw new ValidationException
            {
                ErrorMessage = "You are not the owner of this vehicle.",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        
        if (vehicle.BatteryCapacity != request.BatteryIds.Count)
            throw new ValidationException
            {
                ErrorMessage = "Battery capacity does not match.",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        var stationActive = await context.Stations.AnyAsync(s => s.IsActive && s.StationId == request.StationId);
        if (!stationActive)
            throw new ValidationException
            {
                ErrorMessage = "Station is not active.",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        var validBatteries = await context.Batteries
            .Include(b => b.StationBatterySlot)
            .Where(b =>
                b.BatteryTypeId == vehicle.BatteryTypeId &&
                b.StationBatterySlot != null &&
                b.StationBatterySlot.StationId == request.StationId &&
                b.StationBatterySlot.Status == SBSStatus.Available)
            .ToListAsync();

        var selected = validBatteries.Where(b => request.BatteryIds.Contains(b.BatteryId)).ToList();
        if (selected.Count != request.BatteryIds.Count)
            throw new ValidationException
            {
                ErrorMessage = "One or more requested batteries are not available.",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        await using var tx = await context.Database.BeginTransactionAsync();
        try
        {
            var booking = new Booking
            {
                BookingId = Guid.NewGuid().ToString(),
                UserId = userId,
                StationId = request.StationId,
                VehicleId = request.VehicleId,
                Status = BBRStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };
            context.Bookings.Add(booking);

            var bookingSlots = new List<BatteryBookingSlot>();
            foreach (var battery in selected)
            {
                if (battery.StationBatterySlot is null)
                    throw new ValidationException
                    {
                        ErrorMessage = "Station battery slot not found for a selected battery.",
                        Code = "400",
                        StatusCode = HttpStatusCode.BadRequest
                    };

                bookingSlots.Add(new BatteryBookingSlot
                {
                    Booking = booking,
                    BatteryId = battery.BatteryId,
                    StationSlotId = battery.StationBatterySlot.StationSlotId,
                    Status = SBSStatus.Available,
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (bookingSlots.Count == 0)
                throw new ValidationException
                {
                    ErrorMessage = "No valid batteries to book.",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };

            context.BatteryBookingSlots.AddRange(bookingSlots);
            await context.SaveChangesAsync();
            await tx.CommitAsync();
        }
        catch
        {
            await tx.RollbackAsync();
            throw;
        }
    }

    public async Task<BookingResponse> GetBookingAsync(string bookingId)
    {
        var b = await context.Bookings
            .Include(x => x.User)
            .Include(x => x.Station)
            .Include(x => x.Vehicle)
            .Include(x => x.BatteryBookingSlots)
                .ThenInclude(s => s.Battery)
                    .ThenInclude(bb => bb.BatteryType)
            .FirstOrDefaultAsync(x => x.BookingId == bookingId);

        if (b is null)
            throw new ValidationException
            {
                ErrorMessage = "Booking not found.",
                Code = "404",
                StatusCode = HttpStatusCode.NotFound
            };

        return ToResponse(b);
    }
    
    public async Task<PaginationWrapper<List<BookingResponse>, BookingResponse>> GetAllMyBookingAsync(int page, int pageSize, string? search)
    {
        var userId = JwtUtils.GetUserId(accessor);
        if (string.IsNullOrEmpty(userId))
        {
            throw new ValidationException
            {
                ErrorMessage = "Unauthorized",
                Code = "401",
                StatusCode = HttpStatusCode.Unauthorized
            };
        }
        
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = context.Bookings
            .Include(x => x.User)
            .Include(x => x.Station)
            .Include(x => x.Vehicle)
            .Include(x => x.BatteryBookingSlots)
            .ThenInclude(s => s.Battery)
            .ThenInclude(bb => bb.BatteryType)
            .AsQueryable();
        
        query = query.Where(b => b.UserId == userId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(b =>
                b.BookingId.Contains(term) ||
                b.User.FullName.Contains(term) ||
                b.User.Email.Contains(term) ||
                b.Station.Name.Contains(term) ||
                b.Vehicle.LicensePlate.Contains(term));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(b => b.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var responses = items.Select(ToResponse).ToList();

     
        return new PaginationWrapper<List<BookingResponse>, BookingResponse>(responses, page, totalItems, pageSize);
    }

    public async Task<PaginationWrapper<List<BookingResponse>, BookingResponse>> GetAllBookingAsync(
        int page, int pageSize, string? search)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var query = context.Bookings
            .Include(x => x.User)
            .Include(x => x.Station)
            .Include(x => x.Vehicle)
            .Include(x => x.BatteryBookingSlots)
                .ThenInclude(s => s.Battery)
                    .ThenInclude(bb => bb.BatteryType)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(b =>
                b.BookingId.Contains(term) ||
                b.User.FullName.Contains(term) ||
                b.User.Email.Contains(term) ||
                b.Station.Name.Contains(term) ||
                b.Vehicle.LicensePlate.Contains(term));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderByDescending(b => b.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var responses = items.Select(ToResponse).ToList();

     
        return new PaginationWrapper<List<BookingResponse>, BookingResponse>(responses, page, totalItems, pageSize);
    }

    private static BookingResponse ToResponse(Booking b)
    {
       
        var firstSlot = b.BatteryBookingSlots.FirstOrDefault();
        var firstBattery = firstSlot?.Battery;
        var batteryType = firstBattery?.BatteryType;

        return new BookingResponse
        {
            // Base
            BookingId = b.BookingId,
            StationId = b.StationId,
            UserId = b.UserId,
            VehicleId = b.VehicleId,
            BatteryId = firstBattery?.BatteryId ?? string.Empty,
            BatteryTypeId = batteryType?.BatteryTypeId ?? string.Empty,
            TimeSlot = string.Empty, 
            Status = b.Status,
            CreatedAt = b.CreatedAt,

            // User
            UserName = b.User.FullName,
            UserEmail = b.User.Email,
            UserPhone = b.User.Phone ?? string.Empty,

            // Station
            StationName = b.Station.Name,
            StationAddress = b.Station.Address,

            // Vehicle
            VehicleBrand = b.Vehicle.VBrand,
            VehicleModel = b.Vehicle.Model,
            LicensePlate = b.Vehicle.LicensePlate,

    
            BatteryTypeName = batteryType?.BatteryTypeName ?? string.Empty,


            ConfirmedByName = null,
            ConfirmedAt = null,
            CompletedAt = null,
            UpdatedAt = null,

            CanCancel = b.Status is BBRStatus.Pending or BBRStatus.Confirmed,
            CanModify = b.Status is BBRStatus.Pending
        };
    }
}