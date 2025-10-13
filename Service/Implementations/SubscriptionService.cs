using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using System.Net;

namespace Service.Implementations
{
    // Implement đúng theo interface ISubscriptionService bạn cung cấp
    public class SubscriptionService(ApplicationDbContext context) : ISubscriptionService
    {
        public async Task<PaginationWrapper<List<SubscriptionResponse>, SubscriptionResponse>> GetAllSubscriptionAsync(
            int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.Subscriptions
                .Include(s => s.User)
                .Include(s => s.SubscriptionPlan)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(s =>
                    s.SubscriptionId.Contains(term) ||
                    s.UserId.Contains(term) ||
                    s.PlanId.Contains(term) ||
                    s.Status.ToString().Contains(term) ||
                    (s.User.FullName != null && s.User.FullName.Contains(term)) ||
                    (s.User.Email != null && s.User.Email.Contains(term)) ||
                    (s.SubscriptionPlan.Name != null && s.SubscriptionPlan.Name.Contains(term)));
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(s => s.Status == SubscriptionStatus.Active) // ưu tiên gói đang Active
                .ThenByDescending(s => s.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = items.Select(Map).ToList();

    
            return new PaginationWrapper<List<SubscriptionResponse>, SubscriptionResponse>(
                responses, page, totalItems, pageSize);
        }

        public async Task<SubscriptionResponse?> GetBySubscriptionAsync(string id)
        {
            var s = await context.Subscriptions
                .Include(x => x.User)
                .Include(x => x.SubscriptionPlan)
                .FirstOrDefaultAsync(x => x.SubscriptionId == id);

            return s is null ? null : Map(s);
        }

        // Interface trả về 1 bản ghi → chọn gói "đang Active" (nếu có), nếu không thì lấy gói mới nhất
        public async Task<SubscriptionResponse> GetByUserAsync(string userId)
        {
            var s = await context.Subscriptions
                .Include(x => x.SubscriptionPlan)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Status == SubscriptionStatus.Active)
                .ThenByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            if (s is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Subscription not found for this user."
                };

            return Map(s);
        }

        public async Task AddAsync(SubscriptionRequest request)
        {
            ValidateRequestDates(request.StartDate, request.EndDate);

            // Ensure user + plan tồn tại
            var userExists = await context.Users.AnyAsync(u => u.UserId == request.UserId);
            if (!userExists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "User does not exist."
                };

            var planExists = await context.SubscriptionPlans.AnyAsync(p => p.PlanId == request.PlanId);
            if (!planExists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Subscription plan does not exist."
                };

            // Một user chỉ có 1 gói Active tại một thời điểm (tùy nghiệp vụ)
            if (request.Status == SubscriptionStatus.Active)
            {
                var hasActive = await context.Subscriptions.AnyAsync(s =>
                    s.UserId == request.UserId && s.Status == SubscriptionStatus.Active);
                if (hasActive)
                    throw new ValidationException
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        Code = "409",
                        ErrorMessage = "User already has an active subscription."
                    };
            }

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
            if (string.IsNullOrWhiteSpace(request.SubscriptionId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "SubscriptionId is required."
                };

            ValidateRequestDates(request.StartDate, request.EndDate);

            var entity = await context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionId == request.SubscriptionId);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Subscription not found."
                };

            // Nếu chuyển sang Active → đảm bảo không có gói Active khác của user
            if (request.Status == SubscriptionStatus.Active)
            {
                var hasAnotherActive = await context.Subscriptions.AnyAsync(s =>
                    s.UserId == entity.UserId &&
                    s.SubscriptionId != entity.SubscriptionId &&
                    s.Status == SubscriptionStatus.Active);
                if (hasAnotherActive)
                    throw new ValidationException
                    {
                        StatusCode = HttpStatusCode.Conflict,
                        Code = "409",
                        ErrorMessage = "User already has another active subscription."
                    };
            }

            // Kiểm tra plan tồn tại nếu đổi
            if (!string.Equals(entity.PlanId, request.PlanId, StringComparison.Ordinal))
            {
                var planExists = await context.SubscriptionPlans.AnyAsync(p => p.PlanId == request.PlanId);
                if (!planExists)
                    throw new ValidationException
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Code = "400",
                        ErrorMessage = "Subscription plan does not exist."
                    };
            }

            entity.PlanId = request.PlanId;
            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Status = request.Status;
            entity.NumberOfSwaps = request.NumberOfSwap;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.Subscriptions.FirstOrDefaultAsync(s => s.SubscriptionId == id);
            if (entity is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Subscription not found."
                };

            context.Subscriptions.Remove(entity);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Có thể bị ràng buộc bởi thanh toán/subscription payment...
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Code = "409",
                    ErrorMessage = "Cannot delete this subscription because it is referenced by other records."
                };
            }
        }

        private static void ValidateRequestDates(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "EndDate must be greater than StartDate."
                };
        }

        private static SubscriptionResponse Map(Subscription s) => new()
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
}