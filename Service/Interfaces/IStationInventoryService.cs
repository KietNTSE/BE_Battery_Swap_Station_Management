using BusinessObject.Dtos;
using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStationInventoryService
    {
        Task<PaginationWrapper<List<StationInventoryResponse>, StationInventoryResponse>> GetAllStationInventoryAsync(int page, int pageSize, string? search);
        Task<StationInventoryResponse?> GetByIdAsync(string id);
        Task<StationInventoryResponse?> GetByStationIdAsync(string stationId);
        Task AddAsync(StationInventoryRequest request);
        Task UpdateAsync(StationInventoryRequest request);
        Task DeleteAsync(string id);
        Task<List<StationInventoryResponse>> GetStationInventoryDetailAsync();
    }
}
