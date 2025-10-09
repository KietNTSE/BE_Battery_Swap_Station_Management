using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class SupportTicketRequest
    {
        public string? TicketId { get; set; }
        public string UserId { get; set; }
        public string? StationId { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;
        public SupportTicketStatus Status { get; set; } = SupportTicketStatus.Open;
    }
    public class SupportTicketResponse
    {
        public string TicketId { get; set; }
        public string UserId { get; set; }
        public string? StationId { get; set; }

        public string Subject { get; set; }
        public string Message { get; set; }
        public Priority Priority { get; set; }
        public SupportTicketStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
