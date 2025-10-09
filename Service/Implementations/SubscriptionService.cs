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
    public class SubscriptionService(ApplicationDbContext context, IHttpContextAccessor accessor): ISubscriptionService
    {
        public async Task<IEnumerable<SubscriptionResponse>> GetAllAsync()
        {
            return await context.Subscriptions
                .Select(s => new SubscriptionResponse
                {
                    SubscriptionId = s.SubscriptionId,
                    UserId = s.UserId,
                    PlanId = s.PlanId,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Status = s.Status,
                    NumberOfSwap = s.NumberOfSwaps,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<SubscriptionResponse?> GetByIdAsync(string id)
        {
            var s = await context.Subscriptions.FindAsync(id);
            if (s == null) return null;

            return new SubscriptionResponse
            {
                SubscriptionId = s.SubscriptionId,
                UserId = s.UserId,
                PlanId = s.PlanId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Status = s.Status,
                NumberOfSwap = s.NumberOfSwaps,
                CreatedAt = s.CreatedAt
            };
        }

        public async Task<IEnumerable<SubscriptionResponse>> GetByUserAsync(string userId)
        {
            return await context.Subscriptions
                .Where(s => s.UserId == userId)
                .Select(s => new SubscriptionResponse
                {
                    SubscriptionId = s.SubscriptionId,
                    UserId = s.UserId,
                    PlanId = s.PlanId,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate,
                    Status = s.Status,
                    NumberOfSwap = s.NumberOfSwaps,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();
        }

        public async Task AddAsync(SubscriptionRequest request)
        {
            var entity = new Subscription
            {
                SubscriptionId = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                PlanId = request.PlanId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                NumberOfSwaps = request.NumberOfSwap,
                CreatedAt = DateTime.UtcNow
            };

            context.Subscriptions.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubscriptionRequest request)
        {
            var s = await context.Subscriptions.FindAsync(request.SubscriptionId);
            if (s == null) throw new Exception("Subscription not found.");

            s.PlanId = request.PlanId;
            s.StartDate = request.StartDate;
            s.EndDate = request.EndDate;
            s.Status = request.Status;
            s.NumberOfSwaps = request.NumberOfSwap;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var s = await context.Subscriptions.FindAsync(id);
            if (s == null) throw new Exception("Subscription not found.");

            context.Subscriptions.Remove(s);
            await context.SaveChangesAsync();
        }
    }
}
