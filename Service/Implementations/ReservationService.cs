using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class ReservationService(ApplicationDbContext context, IHttpContextAccessor accessor) : IReservationService
    {
        public async Task<IEnumerable<ReservationResponse>> GetAllAsync()
        {
            return await context.Reservations
                .Select(r => new ReservationResponse
                {
                    ReservationId = r.ReservationId,
                    StationInventoryId = r.StationInventoryId,
                    Status = r.Status,
                    ReservedAt = r.ReservedAt,
                    ExpiredAt = r.ExpiredAt
                })
                .ToListAsync();
        }

        public async Task<ReservationResponse?> GetByIdAsync(string id)
        {
            var r = await context.Reservations.FindAsync(id);
            if (r == null) return null;

            return new ReservationResponse
            {
                ReservationId = r.ReservationId,
                StationInventoryId = r.StationInventoryId,
                Status = r.Status,
                ReservedAt = r.ReservedAt,
                ExpiredAt = r.ExpiredAt
            };
        }

        public async Task<IEnumerable<ReservationResponse>> GetByStationInventoryAsync(string stationInventoryId)
        {
            return await context.Reservations
                .Where(r => r.StationInventoryId == stationInventoryId)
                .Select(r => new ReservationResponse
                {
                    ReservationId = r.ReservationId,
                    StationInventoryId = r.StationInventoryId,
                    Status = r.Status,
                    ReservedAt = r.ReservedAt,
                    ExpiredAt = r.ExpiredAt
                })
                .ToListAsync();
        }

        public async Task AddAsync(ReservationRequest request)
        {
            var entity = new Reservation
            {
                ReservationId = Guid.NewGuid().ToString(),
                StationInventoryId = request.StationInventoryId,
                Status = request.Status,
                ReservedAt = request.ReservedAt,
                ExpiredAt = request.ExpiredAt
            };

            context.Reservations.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReservationRequest request)
        {
            var r = await context.Reservations.FindAsync(request.ReservationId);
            if (r == null) throw new Exception("Reservation not found.");

            r.Status = request.Status;
            r.ReservedAt = request.ReservedAt;
            r.ExpiredAt = request.ExpiredAt;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var r = await context.Reservations.FindAsync(id);
            if (r == null) throw new Exception("Reservation not found.");

            context.Reservations.Remove(r);
            await context.SaveChangesAsync();
        }
    }

}
