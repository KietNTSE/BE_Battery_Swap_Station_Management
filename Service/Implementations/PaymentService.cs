using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class PaymentService(ApplicationDbContext context, IHttpContextAccessor accessor) : IPaymentService
    {
        public async Task<IEnumerable<PaymentResponse>> GetAllAsync()
        {
            return await context.Payments
                .Select(p => new PaymentResponse
                {
                    PayId = p.PayId,
                    UserId = p.UserId,
                    SwapId = p.SwapId,
                    OrderCode = p.OrderCode,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<PaymentResponse?> GetByIdAsync(string id)
        {
            var p = await context.Payments.FindAsync(id);
            if (p == null) return null;

            return new PaymentResponse
            {
                PayId = p.PayId,
                UserId = p.UserId,
                SwapId = p.SwapId,
                OrderCode = p.OrderCode,
                Amount = p.Amount,
                Currency = p.Currency,
                PaymentMethod = p.PaymentMethod,
                Status = p.Status,
                CreatedAt = p.CreatedAt
            };
        }

        public async Task<IEnumerable<PaymentResponse>> GetByUserAsync(string userId)
        {
            return await context.Payments
                .Where(p => p.UserId == userId)
                .Select(p => new PaymentResponse
                {
                    PayId = p.PayId,
                    UserId = p.UserId,
                    SwapId = p.SwapId,
                    OrderCode = p.OrderCode,
                    Amount = p.Amount,
                    Currency = p.Currency,
                    PaymentMethod = p.PaymentMethod,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task AddAsync(PaymentRequest request)
        {
            var entity = new Payment
            {
                PayId = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                SwapId = request.SwapId,
                OrderCode = request.OrderCode,
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethod = request.PaymentMethod,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow
            };

            context.Payments.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentRequest request)
        {
            var p = await context.Payments.FindAsync(request.PayId);
            if (p == null) throw new Exception("Payment not found.");

            p.Amount = request.Amount;
            p.Currency = request.Currency;
            p.PaymentMethod = request.PaymentMethod;
            p.Status = request.Status;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var p = await context.Payments.FindAsync(id);
            if (p == null) throw new Exception("Payment not found.");

            context.Payments.Remove(p);
            await context.SaveChangesAsync();
        }
    }

}

