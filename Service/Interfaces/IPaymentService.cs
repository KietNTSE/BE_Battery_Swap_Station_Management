using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject.DTOs;
using BusinessObject.Entities;
using BusinessObject.Enums;

namespace Service.Interfaces
{
    public interface IPaymentService
    {
        /// <summary>
        /// Tạo mới Payment cho BatterySwap.
        /// </summary>
        /// <param name="request">Thông tin yêu cầu tạo Payment</param>
        /// <returns>Thông tin Payment vừa tạo</returns>
        Task<PaymentResponse> CreatePaymentAsync(PaymentCreateRequest request);

        /// <summary>
        /// Lấy chi tiết một Payment.
        /// </summary>
        /// <param name="paymentId">Id của Payment</param>
        /// <returns>Thông tin Payment chi tiết</returns>
        Task<PaymentResponse> GetPaymentDetailAsync(string paymentId);

        /// <summary>
        /// Tích hợp PayOS, trả về thông tin QR/link thanh toán của Payment.
        /// </summary>
        /// <param name="paymentId">Id của Payment</param>
        /// <returns>Thông tin trả về từ PayOS</returns>
        Task<PayOSResponseDto> CreatePayOSOrderAsync(string paymentId);

        /// <summary>
        /// Cập nhật trạng thái Payment (Pending/Completed/Cancelled/Failed).
        /// </summary>
        /// <param name="paymentId">Id của Payment</param>
        /// <param name="status">Trạng thái cần cập nhật</param>
        /// <returns>Thông tin Payment sau cập nhật</returns>
        Task<PaymentResponse> UpdatePaymentStatusAsync(string paymentId, PayStatus status);

        /// <summary>
        /// Lấy danh sách Payment liên quan đến một BatterySwap.
        /// </summary>
        /// <param name="swapId">Id của BatterySwap</param>
        /// <returns>Danh sách Payment thuộc BatterySwap đó</returns>
        Task<IList<PaymentResponse>> GetPaymentsBySwapIdAsync(string swapId);
    }
}
