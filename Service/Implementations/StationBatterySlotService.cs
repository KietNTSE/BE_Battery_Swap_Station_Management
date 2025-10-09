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
    public class StationBatterySlotService(ApplicationDbContext context, IHttpContextAccessor accessor)
       : IStationBatterySlotService
    {
        public async Task<IEnumerable<StationBatterySlotResponse>> GetAllAsync()
        {
            return await context.StationBatterySlots
                .Select(s => new StationBatterySlotResponse
                {
                    StationSlotId = s.StationSlotId,
                    StationId = s.StationId,
                    BatteryId = s.BatteryId,
                    SlotNo = s.SlotNo,
                    Status = s.Status,
                    LastUpdated = s.LastUpdated
                })
                .ToListAsync();
        }

        public async Task<StationBatterySlotResponse?> GetByIdAsync(string id)
        {
            var s = await context.StationBatterySlots.FindAsync(id);
            if (s == null) return null;

            return new StationBatterySlotResponse
            {
                StationSlotId = s.StationSlotId,
                StationId = s.StationId,
                BatteryId = s.BatteryId,
                SlotNo = s.SlotNo,
                Status = s.Status,
                LastUpdated = s.LastUpdated
            };
        }

        public async Task<IEnumerable<StationBatterySlotResponse>> GetByStationAsync(string stationId)
        {
            return await context.StationBatterySlots
                .Where(s => s.StationId == stationId)
                .Select(s => new StationBatterySlotResponse
                {
                    StationSlotId = s.StationSlotId,
                    StationId = s.StationId,
                    BatteryId = s.BatteryId,
                    SlotNo = s.SlotNo,
                    Status = s.Status,
                    LastUpdated = s.LastUpdated
                })
                .ToListAsync();
        }

        public async Task AddAsync(StationBatterySlotRequest request)
        {
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
            var s = await context.StationBatterySlots.FindAsync(request.StationSlotId);
            if (s == null) throw new Exception("StationBatterySlot not found.");

            s.BatteryId = request.BatteryId;
            s.SlotNo = request.SlotNo;
            s.Status = request.Status;
            s.LastUpdated = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var s = await context.StationBatterySlots.FindAsync(id);
            if (s == null) throw new Exception("StationBatterySlot not found.");

            context.StationBatterySlots.Remove(s);
            await context.SaveChangesAsync();
        }
    }
}
