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
   
    public class StationBatterySlotService(ApplicationDbContext context) : IStationBatterySlotService
    {
        
        public async Task<PaginationWrapper<List<StationInventoryResponse>, StationInventoryResponse>> GetAllStationInventoryAsync(
            int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.StationInventories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                
                query = query.Where(i =>
                    i.StationInventoryId.Contains(term) ||
                    i.StationId.Contains(term));
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(i => i.LastUpdate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = items.Select(i => new StationInventoryResponse
            {
                StationInventoryId = i.StationInventoryId,
                StationId = i.StationId,
                MaintenanceCount = i.MaintenanceCount,
                FullCount = i.FullCount,
                ChargingCount = i.ChargingCount,
                LastUpdated = i.LastUpdate
            }).ToList();

           
            return new PaginationWrapper<List<StationInventoryResponse>, StationInventoryResponse>(
                responses, page, totalItems, pageSize);
        }

      
        public async Task<StationBatterySlotResponse?> GetByIdAsync(string id)
        {
            var s = await context.StationBatterySlots.FirstOrDefaultAsync(x => x.StationSlotId == id);
            return s is null ? null : ToResponse(s);
        }

        
        public async Task<StationBatterySlotResponse> GetByStationAsync(string stationId)
        {
            var slot = await context.StationBatterySlots
                .Where(x => x.StationId == stationId)
                .OrderBy(x => x.SlotNo)
                .FirstOrDefaultAsync();

            if (slot is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "No station battery slot found for this station."
                };

            return ToResponse(slot);
        }

        public async Task AddAsync(StationBatterySlotRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StationId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "StationId is required."
                };

           
            var duplicated = await context.StationBatterySlots.AnyAsync(s =>
                s.StationId == request.StationId && s.SlotNo == request.SlotNo);
            if (duplicated)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Code = "409",
                    ErrorMessage = "Slot number already exists in this station."
                };

            var entity = new StationBatterySlot
            {
                StationSlotId = Guid.NewGuid().ToString(),
                StationId = request.StationId,
                BatteryId = request.BatteryId,
                SlotNo = request.SlotNo,
                Status = request.Status,
                LastUpdated = DateTime.UtcNow
            };

            context.StationBatterySlots.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StationBatterySlotRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StationSlotId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "StationSlotId is required."
                };

            var entity = await context.StationBatterySlots.FirstOrDefaultAsync(s =>
                s.StationSlotId == request.StationSlotId);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Station battery slot not found."
                };

            
            var duplicated = await context.StationBatterySlots.AnyAsync(s =>
                s.StationId == entity.StationId &&
                s.SlotNo == request.SlotNo &&
                s.StationSlotId != entity.StationSlotId);
            if (duplicated)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Code = "409",
                    ErrorMessage = "Slot number already exists in this station."
                };

            entity.BatteryId = request.BatteryId;
            entity.SlotNo = request.SlotNo;
            entity.Status = request.Status;
            entity.LastUpdated = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.StationBatterySlots.FirstOrDefaultAsync(s => s.StationSlotId == id);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Station battery slot not found."
                };

            context.StationBatterySlots.Remove(entity);
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
                    ErrorMessage = "Cannot delete this slot because it is referenced by other records."
                };
            }
        }

        private static StationBatterySlotResponse ToResponse(StationBatterySlot s) => new()
        {
            StationSlotId = s.StationSlotId,
            StationId = s.StationId,
            BatteryId = s.BatteryId,
            SlotNo = s.SlotNo,
            Status = s.Status,
            LastUpdated = s.LastUpdated
        };
    }
}