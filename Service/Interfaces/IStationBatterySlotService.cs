using BusinessObject.Dtos;
using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStationBatterySlotService
    {
        Task<PaginationWrapper<List<StationBatterySlotResponse>, StationBatterySlotResponse>> GetAllStationSlotAsync(int page, int pageSize, string? search);
        Task<StationBatterySlotResponse?> GetByIdAsync(string id);
        Task<StationBatterySlotResponse> GetByStationAsync(string stationId);
        Task AddAsync(StationBatterySlotRequest request);
        Task UpdateAsync(StationBatterySlotRequest request);
        Task DeleteAsync(string id);
        Task<List<StationBatterySlotResponse>> GetStationBatterySlotDetailAsync();
    }
}
