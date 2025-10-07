using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class BatteryService(ApplicationDbContext context, IHttpContextAccessor accessor) : IBatteryService
    {
        public async Task<IEnumerable<BatteryResponse>> GetAllAsync()
        {
            var batteries = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .AsNoTracking()
                .ToListAsync();

            return batteries.Select(ToResponse);
        }

        //Lấy pin theo ID
        public async Task<BatteryResponse?> GetByIdAsync(string id)
        {
            var battery = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .FirstOrDefaultAsync(b => b.BatteryId == id);

            return battery == null ? null : ToResponse(battery);
        }

        //Lấy pin theo số serial
        public async Task<BatteryResponse?> GetBySerialAsync(int serialNo)
        {
            var battery = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .FirstOrDefaultAsync(b => b.SerialNo == serialNo);

            return battery == null ? null : ToResponse(battery);
        }

        //Lấy pin theo trạm
        public async Task<IEnumerable<BatteryResponse>> GetByStationAsync(string stationId)
        {
            var batteries = await context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .Where(b => b.StationId == stationId)
                .AsNoTracking()
                .ToListAsync();

            return batteries.Select(ToResponse);
        }

        //Lấy danh sách pin có sẵn (Available)
        public async Task<IEnumerable<BatteryResponse>> GetAvailableAsync(string? stationId = null)
        {
            var query = context.Batteries
                .Include(b => b.Station)
                .Include(b => b.BatteryType)
                .Where(b => b.Status == BatteryStatus.Available)
                .AsQueryable();

            if (!string.IsNullOrEmpty(stationId))
                query = query.Where(b => b.StationId == stationId);

            var list = await query.AsNoTracking().ToListAsync();
            return list.Select(ToResponse);
        }

        //Thêm mới pin
        public async Task AddAsync(BatteryRequest request)
        {
            var entity = new Battery
            {
                BatteryId = Guid.NewGuid().ToString(),
                SerialNo = request.SerialNo,
                Owner = request.Owner,
                Status = request.Status,
                Voltage = request.Voltage,
                CapacityWh = request.CapacityWh,
                ImageUrl = request.ImageUrl,
                StationId = request.StationId,
                BatteryTypeId = request.BatteryTypeId,
                UserId = request.UserId,
                ReservationId = request.ReservationId,
                CreatedAt = DateTime.UtcNow
            };

            context.Batteries.Add(entity);
            await context.SaveChangesAsync();
        }

        //Cập nhật pin
        public async Task UpdateAsync(BatteryRequest request)
        {
            var battery = await context.Batteries.FirstOrDefaultAsync(b => b.SerialNo == request.SerialNo);
            if (battery == null)
                throw new Exception("Battery not found.");

            battery.Owner = request.Owner;
            battery.Status = request.Status;
            battery.Voltage = request.Voltage;
            battery.CapacityWh = request.CapacityWh;
            battery.ImageUrl = request.ImageUrl;
            battery.StationId = request.StationId;
            battery.BatteryTypeId = request.BatteryTypeId;
            battery.UserId = request.UserId;
            battery.ReservationId = request.ReservationId;
            battery.UpdatedAt = DateTime.UtcNow;

            context.Batteries.Update(battery);
            await context.SaveChangesAsync();
        }

        //Xóa pin
        public async Task DeleteAsync(string id)
        {
            var battery = await context.Batteries.FirstOrDefaultAsync(b => b.BatteryId == id);
            if (battery == null)
                throw new Exception("Battery not found.");

            context.Batteries.Remove(battery);
            await context.SaveChangesAsync();
        }

        //Hàm chuyển đổi Entity → DTO
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

