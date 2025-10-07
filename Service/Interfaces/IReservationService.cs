using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationResponse>> GetAllAsync();
        Task<ReservationResponse?> GetByIdAsync(string id);
        Task<IEnumerable<ReservationResponse>> GetByStationInventoryAsync(string stationInventoryId);
        Task AddAsync(ReservationRequest request);
        Task UpdateAsync(ReservationRequest request);
        Task DeleteAsync(string id);
    }
}
