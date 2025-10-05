using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Dtos;

namespace Service.Interfaces
{
    public interface IBatteryService
    {
        Task<IEnumerable<BatteryResponse>> GetAllAsync();
        Task<BatteryResponse?> GetByIdAsync(string id);
        Task<BatteryResponse?> GetBySerialAsync(int serialNo);
        Task<IEnumerable<BatteryResponse>> GetByStationAsync(string stationId);
        Task<IEnumerable<BatteryResponse>> GetAvailableAsync(string? stationId = null);
        Task AddAsync(BatteryRequest battery);
        Task UpdateAsync(BatteryRequest battery);
        Task DeleteAsync(string id);

    }
}
