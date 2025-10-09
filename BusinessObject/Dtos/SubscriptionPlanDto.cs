using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class SubscriptionPlanRequest
    {
        public string? PlanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double MonthlyFee { get; set; }
        public string SwapsIncluded { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public int SwapAmount { get; set; }
    }
    public class SubscriptionPlanResponse
    {
        public string PlanId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double MonthlyFee { get; set; }
        public string SwapsIncluded { get; set; } = string.Empty;
        public bool Active { get; set; }
        public int SwapAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }


}
