using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IStationInventoryService
    {
        Task<IEnumerable<StationInventoryResponse>> GetAllAsync();
        Task<StationInventoryResponse?> GetByIdAsync(string id);
        Task<StationInventoryResponse?> GetByStationIdAsync(string stationId);
        Task AddAsync(StationInventoryRequest request);
        Task UpdateAsync(StationInventoryRequest request);
        Task DeleteAsync(string id);
    }
}
