using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Exceptions;
using System.Net;

namespace Service.Implementations
{
    public class ReviewService(ApplicationDbContext context, IHttpContextAccessor accessor) : IReviewService
    {
        public async Task<IEnumerable<ReviewResponse>> GetAllAsync()
        {
            return await context.Reviews
                .Select(r => new ReviewResponse
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    StationId = r.StationId,
                    SwapId = r.SwapId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewResponse>> GetByStationAsync(string stationId)
        {
            return await context.Reviews
                .Where(r => r.StationId == stationId)
                .Select(r => new ReviewResponse
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    StationId = r.StationId,
                    SwapId = r.SwapId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReviewResponse>> GetByUserAsync(string userId)
        {
            return await context.Reviews
                .Where(r => r.UserId == userId)
                .Select(r => new ReviewResponse
                {
                    ReviewId = r.ReviewId,
                    UserId = r.UserId,
                    StationId = r.StationId,
                    SwapId = r.SwapId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<ReviewResponse?> GetByIdAsync(string id)
        {
            var r = await context.Reviews.FindAsync(id);
            if (r == null) return null;
            return new ReviewResponse
            {
                ReviewId = r.ReviewId,
                UserId = r.UserId,
                StationId = r.StationId,
                SwapId = r.SwapId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            };
        }

        public async Task AddAsync(ReviewRequest review)
        {
            // Validate
            if (review.Rating < 1 || review.Rating > 5)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Rating must be between 1-5"
                };
            if (string.IsNullOrWhiteSpace(review.UserId) ||
                string.IsNullOrWhiteSpace(review.StationId) ||
                string.IsNullOrWhiteSpace(review.SwapId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "UserId, StationId và SwapId is obligatory"
                };

            //Kiểm tra swap tồn tại, đúng user, đúng station và đã Completed
            var swap = await context.BatterySwaps
                .FirstOrDefaultAsync(bs => bs.SwapId == review.SwapId
                                           && bs.UserId == review.UserId
                                           && bs.StationId == review.StationId
                                           && bs.Status == BBRStatus.Completed);
            if (swap == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "You must successfully change the battery to review."
                };

            //Mỗi swap chỉ review 1 lần
            var exists = await context.Reviews
                .AnyAsync(r => r.SwapId == review.SwapId && r.UserId == review.UserId);
            if (exists)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "You have already reviewed this battery swap."
                };

            var entity = new Review
            {
                ReviewId = Guid.NewGuid().ToString(),
                UserId = review.UserId,
                StationId = review.StationId,
                SwapId = review.SwapId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = DateTime.UtcNow
            };

            context.Reviews.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReviewRequest review)
        {
            if (string.IsNullOrWhiteSpace(review.ReviewId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "ReviewId is obligatory"
                };

            var entity = await context.Reviews.FindAsync(review.ReviewId);
            if (entity == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Review is not exist"
                };

            if (review.Rating < 1 || review.Rating > 5)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Rating must be between 1-5"
                };

            entity.Rating = review.Rating;
            entity.Comment = review.Comment;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.Reviews.FindAsync(id);
            if (entity == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Review is not exist"
                };

            context.Reviews.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}