using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using System.Net;

namespace Service.Implementations
{
    public class ReservationService(ApplicationDbContext context, IHttpContextAccessor accessor) : IReservationService
    {
        public async Task<PaginationWrapper<List<ReservationResponse>, ReservationResponse>> GetAllReservationsAsync(
            int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.Reservations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(r =>
                    r.ReservationId.Contains(term) ||
                    r.StationInventoryId.Contains(term) ||
                    r.Status.ToString().Contains(term));
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.ReservedAt)
                .ThenByDescending(r => r.ExpiredAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = items.Select(ToResponse).ToList();

            return new PaginationWrapper<List<ReservationResponse>, ReservationResponse>(
                responses, page, totalItems, pageSize);
        }

        public async Task<ReservationResponse?> GetByIdAsync(string id)
        {
            var r = await context.Reservations.FirstOrDefaultAsync(x => x.ReservationId == id);
            return r is null ? null : ToResponse(r);
        }

        public async Task<ReservationResponse> GetByStationInventoryAsync(string stationInventoryId)
        {
            var r = await context.Reservations
                .Where(x => x.StationInventoryId == stationInventoryId)
                .OrderByDescending(x => x.ReservedAt)
                .FirstOrDefaultAsync();

            if (r is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Reservation not found for this station inventory."
                };

            return ToResponse(r);
        }

        public async Task AddAsync(ReservationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StationInventoryId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "StationInventoryId is required."
                };

            if (request.ExpiredAt <= request.ReservedAt)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "ExpiredAt must be greater than ReservedAt."
                };

            var existsInventory = await context.StationInventories
                .AnyAsync(si => si.StationInventoryId == request.StationInventoryId);
            if (!existsInventory)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "StationInventory does not exist."
                };

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
            if (string.IsNullOrWhiteSpace(request.ReservationId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "ReservationId is required."
                };

            var entity = await context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == request.ReservationId);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Reservation not found."
                };

            if (request.ExpiredAt <= request.ReservedAt)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "ExpiredAt must be greater than ReservedAt."
                };

            entity.StationInventoryId = request.StationInventoryId;
            entity.Status = request.Status;
            entity.ReservedAt = request.ReservedAt;
            entity.ExpiredAt = request.ExpiredAt;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.Reservations
                .FirstOrDefaultAsync(r => r.ReservationId == id);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Reservation not found."
                };

            context.Reservations.Remove(entity);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Code = "409",
                    ErrorMessage = "Cannot delete this reservation because it is referenced by other records."
                };
            }
        }

        public async Task<List<ReservationResponse>> GetReservationDetailAsync()
        {
            var reservations = await context.Reservations.ToListAsync();
            return reservations.Select(ToResponse).ToList();
        }

        private static ReservationResponse ToResponse(Reservation r) => new()
        {
            ReservationId = r.ReservationId,
            StationInventoryId = r.StationInventoryId,
            Status = r.Status,
            ReservedAt = r.ReservedAt,
            ExpiredAt = r.ExpiredAt
        };
    }
}