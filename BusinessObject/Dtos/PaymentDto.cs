// BusinessObject/DTOs/PaymentDto.cs
using BusinessObject.Enums;

namespace BusinessObject.DTOs
{
    public class CreatePaymentRequest
    {
        public string SwapId { get; set; } = string.Empty;
        public PayMethod PaymentMethod { get; set; }
    }

    public class PaymentResponse
    {
        public string PayId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string SwapId { get; set; } = string.Empty;
        public string OrderCode { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public PayMethod PaymentMethod { get; set; }
        public PayStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdatePaymentStatusRequest
    {
        public PayStatus Status { get; set; }
        public string? TransactionId { get; set; } // For tracking
    }

    public class PaymentDetailResponse : PaymentResponse
    {
        public BatterySwapResponse? BatterySwap { get; set; }
    }
}