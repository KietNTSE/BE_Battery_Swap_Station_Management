using BusinessObject.Dtos;
using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationResponse?> GetByIdAsync(string id);
        Task<ReservationResponse> GetByStationInventoryAsync(string stationInventoryId);
        Task AddAsync(ReservationRequest request);
        Task UpdateAsync(ReservationRequest request);
        Task DeleteAsync(string id);
        Task<PaginationWrapper<List<ReservationResponse>, ReservationResponse>> GetAllReservationsAsync(int page,
        int pageSize, string? search);
    }
}
