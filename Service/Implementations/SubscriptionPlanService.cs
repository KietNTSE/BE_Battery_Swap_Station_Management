using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class SubscriptionPlanService(ApplicationDbContext context, IHttpContextAccessor accessor) : ISubscriptionPlanService
    {
        public async Task<IEnumerable<SubscriptionPlanResponse>> GetAllAsync()
        {
            return await context.SubscriptionPlans
                .Select(p => new SubscriptionPlanResponse
                {
                    PlanId = p.PlanId,
                    Name = p.Name,
                    Description = p.Description,
                    MonthlyFee = p.MonthlyFee,
                    SwapsIncluded = p.SwapsIncluded,
                    Active = p.Active,
                    SwapAmount = p.SwapAmount,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<SubscriptionPlanResponse?> GetByIdAsync(string id)
        {
            var p = await context.SubscriptionPlans.FindAsync(id);
            if (p == null) return null;

            return new SubscriptionPlanResponse
            {
                PlanId = p.PlanId,
                Name = p.Name,
                Description = p.Description,
                MonthlyFee = p.MonthlyFee,
                SwapsIncluded = p.SwapsIncluded,
                Active = p.Active,
                SwapAmount = p.SwapAmount,
                CreatedAt = p.CreatedAt
            };
        }

        public async Task AddAsync(SubscriptionPlanRequest request)
        {
            var entity = new SubscriptionPlan
            {
                PlanId = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                MonthlyFee = request.MonthlyFee,
                SwapsIncluded = request.SwapsIncluded,
                Active = request.Active,
                SwapAmount = request.SwapAmount,
                CreatedAt = DateTime.UtcNow
            };

            context.SubscriptionPlans.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SubscriptionPlanRequest request)
        {
            var p = await context.SubscriptionPlans.FindAsync(request.PlanId);
            if (p == null) throw new Exception("SubscriptionPlan not found.");

            p.Name = request.Name;
            p.Description = request.Description;
            p.MonthlyFee = request.MonthlyFee;
            p.SwapsIncluded = request.SwapsIncluded;
            p.Active = request.Active;
            p.SwapAmount = request.SwapAmount;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var p = await context.SubscriptionPlans.FindAsync(id);
            if (p == null) throw new Exception("SubscriptionPlan not found.");

            context.SubscriptionPlans.Remove(p);
            await context.SaveChangesAsync();
        }
    }
}
