using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<SubscriptionResponse>> GetAllAsync();
        Task<SubscriptionResponse?> GetByIdAsync(string id);
        Task<IEnumerable<SubscriptionResponse>> GetByUserAsync(string userId);
        Task AddAsync(SubscriptionRequest request);
        Task UpdateAsync(SubscriptionRequest request);
        Task DeleteAsync(string id);
    }
}
