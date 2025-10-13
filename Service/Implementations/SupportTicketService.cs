using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using System.Net;

namespace Service.Implementations
{
    public class SupportTicketService(ApplicationDbContext context) : ISupportTicketService
    {
        public async Task<PaginationWrapper<List<SupportTicketResponse>, SupportTicketResponse>> GetAllSupportTicketAsync(
            int page, int pageSize, string? search)
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = context.SupportTickets
                .Include(t => t.User)
                .Include(t => t.Station)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var keyword = search.Trim();
                query = query.Where(t =>
                    t.Subject.Contains(keyword) ||
                    t.Message.Contains(keyword) ||
                    (t.User != null && t.User.FullName.Contains(keyword)) ||
                    (t.Station != null && t.Station.Name.Contains(keyword))
                );
            }

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(t => t.UpdatedAt)
                .ThenByDescending(t => t.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var responses = items.Select(ToResponse).ToList();

            // Nếu constructor của PaginationWrapper trong dự án bạn khác tham số,
            // hãy đổi lại thứ tự cho khớp (có nơi dùng (items, total, page, size)).
            return new PaginationWrapper<List<SupportTicketResponse>, SupportTicketResponse>(
                responses, totalItems, page, pageSize);
        }

        public async Task<SupportTicketResponse?> GetBySupportTicketAsync(string id)
        {
            var t = await context.SupportTickets
                .Include(x => x.User)
                .Include(x => x.Station)
                .FirstOrDefaultAsync(x => x.TicketId == id);

            return t == null ? null : ToResponse(t);
        }

        public async Task AddAsync(SupportTicketRequest request)
        {
            // Validate cơ bản
            if (string.IsNullOrWhiteSpace(request.UserId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "UserId is required."
                };
            if (string.IsNullOrWhiteSpace(request.Subject))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Subject is required."
                };
            if (string.IsNullOrWhiteSpace(request.Message))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Message is required."
                };

            var entity = new SupportTicket
            {
                TicketId = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                StationId = request.StationId, // có thể null
                Subject = request.Subject,
                Message = request.Message,
                Priority = request.Priority,
                Status = request.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            context.SupportTickets.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SupportTicketRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.TicketId))
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "TicketId is required."
                };

            var entity = await context.SupportTickets.FirstOrDefaultAsync(t => t.TicketId == request.TicketId);
            if (entity == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Support ticket not found."
                };

            // Cho phép cập nhật các trường sau
            entity.StationId = request.StationId;        // optional
            entity.Subject = request.Subject ?? entity.Subject;
            entity.Message = request.Message ?? entity.Message;
            entity.Priority = request.Priority;
            entity.Status = request.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await context.SupportTickets.FirstOrDefaultAsync(t => t.TicketId == id);
            if (entity == null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Code = "404",
                    ErrorMessage = "Support ticket not found."
                };

            context.SupportTickets.Remove(entity);
            await context.SaveChangesAsync();
        }

        private static SupportTicketResponse ToResponse(SupportTicket t)
        {
            return new SupportTicketResponse
            {
                TicketId = t.TicketId,
                UserId = t.UserId,
                StationId = t.StationId,
                Subject = t.Subject,
                Message = t.Message,
                Priority = t.Priority,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}