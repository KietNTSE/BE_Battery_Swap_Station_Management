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
    public class SupportTicketService(ApplicationDbContext context, IHttpContextAccessor accessor) : ISupportTicketService
    {
        public async Task<IEnumerable<SupportTicketResponse>> GetAllAsync()
        {
            return await context.SupportTickets
                .Select(t => new SupportTicketResponse
                {
                    TicketId = t.TicketId,
                    UserId = t.UserId,
                    StationId = t.StationId,
                    Subject = t.Subject,
                    Message = t.Message,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt ?? t.CreatedAt
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<SupportTicketResponse>> GetByUserAsync(string userId)
        {
            return await context.SupportTickets
                .Where(t => t.UserId == userId)
                .Select(t => new SupportTicketResponse
                {
                    TicketId = t.TicketId,
                    UserId = t.UserId,
                    StationId = t.StationId,
                    Subject = t.Subject,
                    Message = t.Message,
                    Priority = t.Priority,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt ?? t.CreatedAt
                })
                .ToListAsync();
        }


        public async Task<SupportTicketResponse?> GetByIdAsync(string id)
        {
            var t = await context.SupportTickets.FindAsync(id);
            if (t == null) return null;

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
                UpdatedAt = t.UpdatedAt ?? t.CreatedAt
            };
        }

        public async Task AddAsync(SupportTicketRequest request)
        {
            var entity = new SupportTicket
            {
                TicketId = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                StationId = request.StationId,
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
            var t = await context.SupportTickets.FindAsync(request.TicketId);
            if (t == null)
                throw new Exception("Support ticket not found.");

            t.Subject = request.Subject;
            t.Message = request.Message;
            t.Priority = request.Priority;
            t.Status = request.Status;
            t.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var t = await context.SupportTickets.FindAsync(id);
            if (t == null)
                throw new Exception("Support ticket not found.");

            context.SupportTickets.Remove(t);
            await context.SaveChangesAsync();
        }
    }
}
