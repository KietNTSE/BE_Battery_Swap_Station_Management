using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStationBatterySlotService
    {
        Task<IEnumerable<StationBatterySlotResponse>> GetAllAsync();
        Task<StationBatterySlotResponse?> GetByIdAsync(string id);
        Task<IEnumerable<StationBatterySlotResponse>> GetByStationAsync(string stationId);
        Task AddAsync(StationBatterySlotRequest request);
        Task UpdateAsync(StationBatterySlotRequest request);
        Task DeleteAsync(string id);
    }
}
