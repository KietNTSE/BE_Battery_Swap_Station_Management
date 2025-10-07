using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Enums;

namespace BusinessObject.Dtos
{
    public class PaymentRequest
    {
        public string? PayId { get; set; }
        public string UserId { get; set; }
        public string SwapId { get; set; }
        public string OrderCode { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public PayMethod PaymentMethod { get; set; }
        public PayStatus Status { get; set; }
    }

    public class PaymentResponse
    {
        public string PayId { get; set; }
        public string UserId { get; set; }
        public string SwapId { get; set; }
        public string OrderCode { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public PayMethod PaymentMethod { get; set; }
        public PayStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

