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
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            };
        }

        public async Task AddAsync(ReviewRequest review)
        {
            var entity = new Review
            {
                ReviewId = Guid.NewGuid().ToString(),
                UserId = review.UserId,
                StationId = review.StationId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = DateTime.UtcNow
            };
            context.Reviews.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ReviewRequest review)
        {
            var entity = await context.Reviews.FindAsync(review.ReviewId);
            if (entity == null)
            {
                throw new Exception("Review not found.");
            }
            entity.Rating = review.Rating;
            entity.Comment = review.Comment;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.Reviews.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Review not found.");
            }
            context.Reviews.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
