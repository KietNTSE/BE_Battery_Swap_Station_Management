using System.Net;
using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    
    public class BatteryService(ApplicationDbContext context) : IBatteryService
    {
        public async Task<BatteryResponse> GetByBatteryAsync(string id)
        {
            var battery = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .FirstOrDefaultAsync(b => b.BatteryId == id);

           if (battery is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Battery not found."
                };
           return ToResponse(battery);
        }

        public async Task<BatteryResponse> GetBySerialAsync(int serialNo)
        {
            var battery = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .FirstOrDefaultAsync(b => b.SerialNo == serialNo);

            if (battery is null)
            {
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Battery not found."
                };
            }
            return ToResponse(battery);
        }

       
        public async Task<BatteryResponse> GetByStationAsync(string stationId)
        {
            var battery = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .Where(b => b.StationId == stationId)
                .OrderByDescending(b => b.UpdatedAt ?? b.CreatedAt)
                .FirstOrDefaultAsync();

            if (battery == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "No battery found for the given station."
                };

            return ToResponse(battery);
        }

       
        public async Task<BatteryResponse> GetAvailableAsync(string? stationId = null)
        {
            var query = context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .Where(b => b.Status == BatteryStatus.Available);

            if (!string.IsNullOrEmpty(stationId))
                query = query.Where(b => b.StationId == stationId);

            var battery = await query
                .OrderByDescending(b => b.UpdatedAt ?? b.CreatedAt)
                .FirstOrDefaultAsync();

            if (battery == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "No available battery found."
                };

            return ToResponse(battery);
        }

        public async Task AddAsync(BatteryRequest request)
        {
            // Đảm bảo SerialNo unique
            var exists = await context.Batteries.AnyAsync(b => b.SerialNo == request.SerialNo);
            if (exists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Battery with the same SerialNo already exists."
                };

            var entity = new Battery
            {
                BatteryId = Guid.NewGuid().ToString(),
                SerialNo = request.SerialNo,
                Owner = request.Owner,
                Status = request.Status,
                Voltage = request.Voltage,
                CapacityWh = request.CapacityWh,
                ImageUrl = request.ImageUrl,
                StationId = request.StationId ?? string.Empty,
                BatteryTypeId = request.BatteryTypeId ?? string.Empty,
                UserId = request.UserId ?? string.Empty,
                ReservationId = request.ReservationId,
                CreatedAt = DateTime.UtcNow
            };

            context.Batteries.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BatteryRequest request)
        {
           
            var battery = await context.Batteries
                .FirstOrDefaultAsync(b => b.SerialNo == request.SerialNo);

            if (battery == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Battery not found."
                };

            battery.Owner = request.Owner;
            battery.Status = request.Status;
            battery.Voltage = request.Voltage;
            battery.CapacityWh = request.CapacityWh;
            battery.ImageUrl = request.ImageUrl;
            battery.StationId = request.StationId ?? battery.StationId;
            battery.BatteryTypeId = request.BatteryTypeId ?? battery.BatteryTypeId;
            battery.UserId = request.UserId ?? battery.UserId;
            battery.ReservationId = request.ReservationId;
            battery.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var battery = await context.Batteries.FirstOrDefaultAsync(b => b.BatteryId == id);
            if (battery == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Battery not found."
                };

            context.Batteries.Remove(battery);
            await context.SaveChangesAsync();
        }

        public async Task<PaginationWrapper<List<BatteryResponse>, BatteryResponse>> GetAllBatteriesAsync(
            int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(b =>
                    b.SerialNo.ToString().Contains(term) ||
                    (b.Station != null && b.Station.Name.Contains(term)) ||
                    (b.BatteryType != null && b.BatteryType.BatteryTypeName.Contains(term))
                );
            }

            var totalItems = await query.CountAsync();

            var data = await query
                .OrderByDescending(b => b.UpdatedAt ?? b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = data.Select(ToResponse).ToList();

            return new PaginationWrapper<List<BatteryResponse>, BatteryResponse>(responses, totalItems, page, pageSize);
        }

        
        private static BatteryResponse ToResponse(Battery b)
        {
            return new BatteryResponse
            {
                BatteryId = b.BatteryId,
                SerialNo = b.SerialNo,
                Owner = b.Owner,
                Status = b.Status,
                Voltage = b.Voltage,
                CapacityWh = b.CapacityWh,
                ImageUrl = b.ImageUrl,
                StationId = b.StationId,
                StationName = b.Station?.Name,
                BatteryTypeId = b.BatteryTypeId,
                BatteryTypeName = b.BatteryType?.BatteryTypeName,
                UserId = b.UserId,
                ReservationId = b.ReservationId,
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            };
        }
    }
}