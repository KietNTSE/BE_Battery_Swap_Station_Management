using BusinessObject.Enums;
using System;

namespace BusinessObject.DTOs
{
    public class PayOSRequestDto
    {
        public string OrderCode { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public PayMethod PaymentMethod { get; set; } = PayMethod.Bank_Transfer;
        public string? Description { get; set; }
        public string? RedirectUrl { get; set; }
        public string? CallbackUrl { get; set; }

        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
    }

    public class PayOSResponseDto
    {
        public string OrderCode { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Currency { get; set; } = "VND";
        public PayMethod PaymentMethod { get; set; }
        public PayStatus Status { get; set; }
        public string? PaymentUrl { get; set; }       // Link QR thanh toán hoặc link PayOS trả về
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? TransactionId { get; set; }
    }
}
