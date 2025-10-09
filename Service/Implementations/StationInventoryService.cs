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
    public class StationInventoryService(ApplicationDbContext context, IHttpContextAccessor accessor) : IStationInventoryService
    {
        public async Task<IEnumerable<StationInventoryResponse>> GetAllAsync()
        {
            return await context.StationInventories
                .Select(s => new StationInventoryResponse
                {
                    StationInventoryId = s.StationInventoryId,
                    StationId = s.StationId,
                    MaintenanceCount = s.MaintenanceCount,
                    FullCount = s.FullCount,
                    ChargingCount = s.ChargingCount,
                    LastUpdated = s.LastUpdate
                })
                .ToListAsync();
        }

        public async Task<StationInventoryResponse?> GetByIdAsync(string id)
        {
            var s = await context.StationInventories.FindAsync(id);
            if (s == null) return null;

            return new StationInventoryResponse
            {
                StationInventoryId = s.StationInventoryId,
                StationId = s.StationId,
                MaintenanceCount = s.MaintenanceCount,
                FullCount = s.FullCount,
                ChargingCount = s.ChargingCount,
                LastUpdated = s.LastUpdate
            };
        }

        public async Task<StationInventoryResponse?> GetByStationIdAsync(string stationId)
        {
            var s = await context.StationInventories
                .FirstOrDefaultAsync(i => i.StationId == stationId);

            if (s == null) return null;

            return new StationInventoryResponse
            {
                StationInventoryId = s.StationInventoryId,
                StationId = s.StationId,
                MaintenanceCount = s.MaintenanceCount,
                FullCount = s.FullCount,
                ChargingCount = s.ChargingCount,
                LastUpdated = s.LastUpdate
            };
        }

        public async Task AddAsync(StationInventoryRequest request)
        {
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
            var s = await context.StationInventories.FindAsync(request.StationInventoryId);
            if (s == null) throw new Exception("Station inventory not found.");


            s.MaintenanceCount = request.MaintenanceCount;
            s.FullCount = request.FullCount;
            s.ChargingCount = request.ChargingCount;
            s.LastUpdate = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var s = await context.StationInventories.FindAsync(id);
            if (s == null) throw new Exception("Station inventory not found.");

            context.StationInventories.Remove(s);
            await context.SaveChangesAsync();
        }
    }
}

