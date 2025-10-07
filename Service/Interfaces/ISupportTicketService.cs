using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISupportTicketService
    {
        Task<IEnumerable<SupportTicketResponse>> GetAllAsync();
        Task<IEnumerable<SupportTicketResponse>> GetByUserAsync(string userId);
        Task<SupportTicketResponse?> GetByIdAsync(string id);
        Task AddAsync(SupportTicketRequest request);
        Task UpdateAsync(SupportTicketRequest request);
        Task DeleteAsync(string id);
    }
}
