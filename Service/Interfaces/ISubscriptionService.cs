using BusinessObject.Dtos;
using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISubscriptionService
    {
        Task<PaginationWrapper<List<SubscriptionResponse>, SubscriptionResponse>> GetAllSubscriptionAsync(int page,
        int pageSize, string? search);
        Task<SubscriptionResponse?> GetBySubscriptionAsync(string id);
        Task<SubscriptionResponse> GetByUserAsync(string userId);
        Task AddAsync(SubscriptionRequest request);
        Task UpdateAsync(SubscriptionRequest request);
        Task DeleteAsync(string id);
        Task<List<SubscriptionResponse>> GetSubscriptionDetailAsync();
    }
}
