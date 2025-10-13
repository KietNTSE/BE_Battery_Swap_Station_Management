using BusinessObject.Dtos;
using BusinessObject.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISupportTicketService
    {
        Task<PaginationWrapper<List<SupportTicketResponse>, SupportTicketResponse>> GetAllSupportTicketAsync(int page,
        int pageSize, string? search);
        Task<SupportTicketResponse?> GetBySupportTicketAsync(string id);
        Task AddAsync(SupportTicketRequest request);
        Task UpdateAsync(SupportTicketRequest request);
        Task DeleteAsync(string id);
        Task<List<SupportTicketResponse>> GetSupportTicketDetailAsync();
    }
}
