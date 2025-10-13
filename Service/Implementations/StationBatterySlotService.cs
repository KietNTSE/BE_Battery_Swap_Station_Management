﻿using BusinessObject;
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
        public async Task<PaginationWrapper<List<StationBatterySlotResponse>, StationBatterySlotResponse>> GetAllStationSlotAsync(int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.StationBatterySlots
                .Include(s => s.Station)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(s =>
                    s.StationSlotId.Contains(term) ||
                    s.StationId.Contains(term)
                );
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(s => s.StationSlotId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = items.Select(ToSlotResponse).ToList();

            return new PaginationWrapper<List<StationBatterySlotResponse>, StationBatterySlotResponse>( responses, page, totalItems, pageSize);
        }

        public async Task<StationBatterySlotResponse?> GetByIdAsync(string id)
        {
            var slot = await context.StationBatterySlots
                .Include(s => s.Station)
                .FirstOrDefaultAsync(s => s.StationSlotId == id);

            return slot == null ? null : ToSlotResponse(slot);
        }

        public async Task<StationBatterySlotResponse> GetByStationAsync(string stationId)
        {
            var slot = await context.StationBatterySlots
                .Include(s => s.Station)
                .Where(s => s.StationId == stationId)
                .OrderByDescending(s => s.StationSlotId)
                .FirstOrDefaultAsync();

            if (slot == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "No battery slot found for the given station."
                };

            return ToSlotResponse(slot);
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

            var exists = await context.StationBatterySlots.AnyAsync(s =>
                s.StationId == request.StationId &&
                s.SlotNo == request.SlotNo);

            if (exists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "StationBatterySlot with the same station and slot number already exists."
                };

            var entity = new StationBatterySlot
            {
                StationSlotId = Guid.NewGuid().ToString(),
                StationId = request.StationId,
                SlotNo = request.SlotNo,
                Status = request.Status,
                BatteryId = request.BatteryId
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
                    ErrorMessage = "StationBatterySlotId is required."
                };

            var entity = await context.StationBatterySlots.FirstOrDefaultAsync(s => s.StationSlotId == request.StationSlotId);
            if (entity == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "StationBatterySlot not found."
                };

            entity.StationId = request.StationId;
            entity.SlotNo = request.SlotNo;
            entity.Status = request.Status;
            entity.BatteryId = request.BatteryId;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.StationBatterySlots.FirstOrDefaultAsync(s => s.StationSlotId == id);
            if (entity == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "StationBatterySlot not found."
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

        public async Task<List<StationBatterySlotResponse>> GetStationBatterySlotDetailAsync()
        {
            var slots = await context.StationBatterySlots
                .Include(s => s.Station)
                .ToListAsync();

            return slots.Select(ToSlotResponse).ToList();
        }

        // Mapping methods

        private static StationBatterySlotResponse ToSlotResponse(StationBatterySlot s) => new()
        {
            StationSlotId = s.StationSlotId,
            StationId = s.StationId,
            SlotNo = s.SlotNo,
            Status = s.Status,
            BatteryId = s.BatteryId,
        };
    }
}