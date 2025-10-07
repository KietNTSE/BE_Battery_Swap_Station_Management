using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponse>> GetAllAsync();
        Task<IEnumerable<ReviewResponse>> GetByStationAsync(string stationId);
        Task<IEnumerable<ReviewResponse>> GetByUserAsync(string userId);
        Task<ReviewResponse?> GetByIdAsync(string id);
        Task AddAsync(ReviewRequest review);
        Task UpdateAsync(ReviewRequest review);
        Task DeleteAsync(string id);
    }
}
