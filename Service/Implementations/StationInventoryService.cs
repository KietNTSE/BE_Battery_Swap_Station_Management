using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using System.Net;

namespace Service.Implementations
{
    public class StationInventoryService(ApplicationDbContext context) : IStationInventoryService
    {
        public async Task<PaginationWrapper<List<StationInventoryResponse>, StationInventoryResponse>> GetAllStationInventoryAsync(
            int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.StationInventories
                .Include(si => si.Station)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(si =>
                    si.StationInventoryId.Contains(term) ||
                    si.StationId.Contains(term) ||
                    (si.Station != null && si.Station.Name.Contains(term)));
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(si => si.LastUpdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = items.Select(ToResponse).ToList();

           
            return new PaginationWrapper<List<StationInventoryResponse>, StationInventoryResponse>(
                responses, page, totalItems, pageSize);
        }

        public async Task<StationInventoryResponse?> GetByIdAsync(string id)
        {
            var si = await context.StationInventories.FirstOrDefaultAsync(x => x.StationInventoryId == id);
            return si is null ? null : ToResponse(si);
        }

        public async Task<StationInventoryResponse?> GetByStationIdAsync(string stationId)
        {
            var si = await context.StationInventories.FirstOrDefaultAsync(x => x.StationId == stationId);
            return si is null ? null : ToResponse(si);
        }

        public async Task AddAsync(StationInventoryRequest request)
        {
            ValidateCounts(request.MaintenanceCount, request.FullCount, request.ChargingCount);

            // Station phải tồn tại
            var stationExists = await context.Stations.AnyAsync(s => s.StationId == request.StationId);
            if (!stationExists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Station does not exist."
                };

            // Một trạm chỉ nên có 1 inventory
            var exists = await context.StationInventories.AnyAsync(si => si.StationId == request.StationId);
            if (exists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Code = "409",
                    ErrorMessage = "Inventory for this station already exists."
                };

            var entity = new StationInventory
            {
                StationInventoryId = Guid.NewGuid().ToString(),
                StationId = request.StationId,
                MaintenanceCount = request.MaintenanceCount,
                FullCount = request.FullCount,
                ChargingCount = request.ChargingCount,
                LastUpdate = DateTime.UtcNow
            };

            context.StationInventories.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StationInventoryRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StationInventoryId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "StationInventoryId is required."
                };

            ValidateCounts(request.MaintenanceCount, request.FullCount, request.ChargingCount);

            var entity = await context.StationInventories
                .FirstOrDefaultAsync(si => si.StationInventoryId == request.StationInventoryId);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Station inventory not found."
                };

            // Cho phép đổi StationId nếu cần? Thường KHÔNG. Giữ nguyên để tránh xung đột dữ liệu.
            // entity.StationId = request.StationId;

            entity.MaintenanceCount = request.MaintenanceCount;
            entity.FullCount = request.FullCount;
            entity.ChargingCount = request.ChargingCount;
            entity.LastUpdate = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.StationInventories.FirstOrDefaultAsync(si => si.StationInventoryId == id);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Station inventory not found."
                };

            context.StationInventories.Remove(entity);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Có thể bị ràng buộc bởi Reservation hoặc các liên kết khác
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Code = "409",
                    ErrorMessage = "Cannot delete this station inventory because it is referenced by other records."
                };
            }
        }

        private static void ValidateCounts(int maintenance, int full, int charging)
        {
            if (maintenance < 0 || full < 0 || charging < 0)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Counts must be non-negative."
                };
        }

        private static StationInventoryResponse ToResponse(StationInventory si) => new()
        {
            StationInventoryId = si.StationInventoryId,
            StationId = si.StationId,
            MaintenanceCount = si.MaintenanceCount,
            FullCount = si.FullCount,
            ChargingCount = si.ChargingCount,
            LastUpdated = si.LastUpdate
        };
    }
}