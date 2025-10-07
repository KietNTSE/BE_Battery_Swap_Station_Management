using BusinessObject.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentResponse>> GetAllAsync();
        Task<PaymentResponse?> GetByIdAsync(string id);
        Task<IEnumerable<PaymentResponse>> GetByUserAsync(string userId);
        Task AddAsync(PaymentRequest request);
        Task UpdateAsync(PaymentRequest request);
        Task DeleteAsync(string id);
    }
}
