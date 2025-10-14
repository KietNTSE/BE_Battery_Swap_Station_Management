using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Dtos
{
    // REQUESTS

    // B1: Người dùng gửi yêu cầu "quên mật khẩu"
    public class ForgotPasswordRequest
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;
    }

    // B2: Người dùng xác nhận OTP và đặt mật khẩu mới
    public class ResetPasswordRequest
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        // 6 chữ số
        [Required, StringLength(6, MinimumLength = 6)]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "OTP must be 6 digits.")]
        public string Otp { get; set; } = string.Empty;

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required, MinLength(6)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    // RESPONSES

    // Trả về thông điệp chung, tránh lộ email tồn tại hay không
    public class ForgotPasswordResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "If the email exists, an OTP has been sent.";
    }

    // Trả về kết quả đặt lại mật khẩu
    public class ResetPasswordResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Password has been reset successfully.";
    }

    // Dùng cho quản trị/giám sát; KHÔNG chứa OTP/Hash
    public class PasswordResetTokenResponse
    {
        public string ResetId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool Consumed { get; set; }
        public int AttemptCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}