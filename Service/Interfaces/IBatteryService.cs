using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Dtos;
using BusinessObject.DTOs;

namespace Service.Interfaces
{
    public interface IBatteryService
    {
        Task<BatteryResponse?> GetByBatteryAsync(string id);
        Task<BatteryResponse?> GetBySerialAsync(int serialNo);
        Task<BatteryResponse> GetByStationAsync(string stationId);
        Task<BatteryResponse> GetAvailableAsync(string? stationId = null);
        Task AddAsync(BatteryRequest battery);
        Task UpdateAsync(BatteryRequest battery);
        Task DeleteAsync(string id);
        Task<PaginationWrapper<List<BatteryResponse>, BatteryResponse>> GetAllBatteriesAsync(int page,
        int pageSize, string? search);

    }
}
