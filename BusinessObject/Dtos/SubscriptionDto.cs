using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class SubscriptionRequest
    {
        public string? SubscriptionId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public int NumberOfSwap { get; set; }
    }

    public class SubscriptionResponse
    {
        public string SubscriptionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public int NumberOfSwap { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
