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
        if (userId is null)
            throw new ValidationException
            {
                ErrorMessage = "Unauthorized",
                Code = "401",
                StatusCode = HttpStatusCode.Unauthorized
            };

        var vehicle =
            await context.Vehicles.FirstOrDefaultAsync(v =>
                v.UserId.Equals(userId) && v.VehicleId.Equals(request.VehicleId));

        if (vehicle is null)
            throw new ValidationException
            {
                ErrorMessage = "You are not owner of this vehicle",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (vehicle.BatteryCapacity != request.BatteryIds.Count)
            throw new ValidationException
            {
                ErrorMessage = "Battery capacity is not matched",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (!await context.Batteries.AnyAsync(b => b.BatteryId.Equals(request.BatteryIds[0])))
            throw new ValidationException
            {
                ErrorMessage = "Battery is not existed",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        if (!await context.Stations.AnyAsync(s => s.IsActive == true && s.StationId.Equals(request.StationId)))
            throw new ValidationException
            {
                ErrorMessage = "Station is not active",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        await using var transaction = await context.Database.BeginTransactionAsync();

        var validBatteries = await context.Batteries.Include(b => b.StationBatterySlot).Where(b =>
            b.BatteryTypeId == vehicle.BatteryTypeId &&
            b.StationBatterySlot != null
            && b.StationBatterySlot.StationId == request.StationId
            && b.StationBatterySlot.Status == 1
        ).ToListAsync();
        if (validBatteries.Count == 0 || validBatteries.Count < request.BatteryIds.Count)
            throw new ValidationException
            {
                ErrorMessage = "No available battery",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };

        var bookingEntity = new Booking
        {
            UserId = userId,
            StationId = request.StationId,
            VehicleId = request.VehicleId,
            Status = BBRStatus.Pending
        };

        try
        {
            context.Bookings.Add(bookingEntity);
            var bookingSlot = new List<BatteryBookingSlot>();
            for (var i = 0; i < request.BatteryIds.Count; i++)
            {
                var battery = validBatteries[i];
                if (battery.StationBatterySlot is null)
                    throw new ValidationException
                    {
                        ErrorMessage = "Station battery slot is not existed",
                        Code = "400",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                var slot = new BatteryBookingSlot
                {
                    Booking = bookingEntity,
                    BatteryId = battery.BatteryId,
                    StationSlotId = battery.StationBatterySlot.StationSlotId,
                    CreatedAt = DateTime.UtcNow,
                    Status = 1
                };
                bookingSlot.Add(slot);
            }

            if (bookingSlot.Count == 0)
            {
                throw new ValidationException
                {
                    ErrorMessage = "No available battery",
                    Code = "400",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            context.BatteryBookingSlots.AddRange(bookingSlot);
            await context.StationBatterySlots
                .Where(s => request.BatteryIds.Contains(s.BatteryId ?? ""))
                .ExecuteUpdateAsync(s => 
                    s.SetProperty(b => b.Status, 2)
                        .SetProperty(b => b.LastUpdated, DateTime.UtcNow));
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new ValidationException
            {
                ErrorMessage = ex.Message,
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}