using BusinessObject;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentResponse> CreatePaymentAsync(PaymentCreateRequest request)
        {
            var payment = new Payment
            {
                PayId = Guid.NewGuid().ToString(),
                SwapId = request.SwapId,
                UserId = request.UserId,
                OrderCode = request.OrderCode ?? Guid.NewGuid().ToString(),
                Amount = request.Amount,
                Currency = request.Currency ?? "VND",
                PaymentMethod = request.PaymentMethod,
                Status = PayStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return MapToPaymentResponse(payment);
        }

        public async Task<PaymentResponse> GetPaymentDetailAsync(string paymentId)
        {
            var payment = await _context.Payments
                .Include(p => p.User)
                .Include(p => p.BatterySwap)
                .FirstOrDefaultAsync(p => p.PayId == paymentId);

            if (payment == null) throw new KeyNotFoundException("Payment not found");

            return MapToPaymentResponse(payment);
        }

        public async Task<PayOSResponseDto> CreatePayOSOrderAsync(string paymentId)
        {
            var payment = await _context.Payments
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PayId == paymentId);

            if (payment == null) throw new KeyNotFoundException("Payment not found");

            var payOsDto = new PayOSRequestDto
            {
                OrderCode = payment.OrderCode,
                Amount = payment.Amount,
                Currency = payment.Currency,
                PaymentMethod = payment.PaymentMethod,
                Description = $"Thanh toán hoán đổi pin {payment.OrderCode}",
                // Nhớ cấu hình các URL redirect/callback tại đây:
                RedirectUrl = "https://yourdomain.com/payment/redirect",
                CallbackUrl = "https://yourdomain.com/payment/callback",
                CustomerName = payment.User?.FullName,
                CustomerPhone = payment.User?.Phone,
                CustomerEmail = payment.User?.Email
            };

            // Gọi PayOS API/service tích hợp của bạn ở đây (giả lập response):
            var payOsRes = await PayOSUtils.CreateOrderAsync(payOsDto); // PayOSUtils là lớp bạn tự cài đặt tích hợp SDK/API PayOS

            // Có thể lưu response lại vào payment nếu cần
            payment.Status = PayStatus.Pending;
            await _context.SaveChangesAsync();

            return payOsRes;
        }

        public async Task<PaymentResponse> UpdatePaymentStatusAsync(string paymentId, PayStatus status)
        {
            var payment = await _context.Payments.FindAsync(paymentId);

            if (payment == null) throw new KeyNotFoundException("Payment not found");

            payment.Status = status;
            await _context.SaveChangesAsync();

            return MapToPaymentResponse(payment);
        }

        public async Task<IList<PaymentResponse>> GetPaymentsBySwapIdAsync(string swapId)
        {
            var payments = await _context.Payments
                .Where(p => p.SwapId == swapId)
                .Include(p => p.User)
                .ToListAsync();

            return payments.Select(MapToPaymentResponse).ToList();
        }

        // Helper method để map entity sang DTO response
        private PaymentResponse MapToPaymentResponse(Payment payment)
        {
            return new PaymentResponse
            {
                PayId = payment.PayId,
                SwapId = payment.SwapId,
                UserId = payment.UserId,
                UserName = payment.User?.FullName,
                OrderCode = payment.OrderCode,
                Amount = payment.Amount,
                Currency = payment.Currency,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                CreatedAt = payment.CreatedAt,
            };
        }
    }
}
