using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBatteryTypeService
    {
        Task<BatteryTypeResponse?> GetByIdAsync(string id);
        Task AddAsync(BatteryTypeRequest request);
        Task UpdateAsync(BatteryTypeRequest request);
        Task DeleteAsync(string id);
        Task<List<BatteryTypeResponse>> GetAllBatteryTypeAsync();
    }
}
