using BusinessObject;
using BusinessObject.Dtos;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Exceptions;
using Service.Interfaces;
using System.Net;
using static BusinessObject.Dtos.ForgotPasswordResponse;

namespace Service.Implementations
{
    public class PasswordResetService(ApplicationDbContext context, IEmailService emailService, IHttpContextAccessor accessor)
        : IPasswordResetService
    {
        private static string GenerateOtp()
        {
            var rng = Random.Shared.Next(100000, 999999);
            return rng.ToString();
        }

        public async Task<ForgotPasswordResponse> RequestPasswordResetAsync(ForgotPasswordRequest request)
        {
            // Không tiết lộ email có tồn tại hay không
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
            {
                // Luôn trả về thành công với thông điệp chung
                return new ForgotPasswordResponse();
            }

            var otp = GenerateOtp();
            var hash = BCrypt.Net.BCrypt.HashPassword(otp);

            var token = new PasswordResetToken
            {
                ResetId = Guid.NewGuid().ToString(),
                UserId = user.UserId,
                Email = user.Email,
                OtpHash = hash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                Consumed = false,
                AttemptCount = 0,
                CreatedAt = DateTime.UtcNow
            };

            context.PasswordResetTokens.Add(token);
            await context.SaveChangesAsync();

            var subject = "EV Driver - OTP code to reset password";
            var body = $@"
            <p>Hello {user.FullName},</p>
            <p>Your password reset OTP code is: <b>{otp}</b></p>
            <p>OTP is valid for 10 minutes. If not requested by you, please ignore this email.</p>
            <p>Best regards,</p>
            <p>EV Driver Team</p>";

            await emailService.SendAsync(user.Email, subject, body);

            return new ForgotPasswordResponse(); // Success=true, message chung
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "NewPassword and ConfirmPassword do not match."
                };

            // Lấy token mới nhất còn hiệu lực, chưa consumed
            var token = await context.PasswordResetTokens
                .Where(t => t.Email == request.Email && !t.Consumed && t.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(t => t.CreatedAt)
                .FirstOrDefaultAsync();

            if (token is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "OTP invalid or expired."
                };

            // Giới hạn số lần thử
            if (token.AttemptCount >= 5)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "You have entered the wrong OTP more than the allowed number of times. Please request a new OTP."
                };

            var otpOk = BCrypt.Net.BCrypt.Verify(request.Otp, token.OtpHash);
            if (!otpOk)
            {
                token.AttemptCount += 1;
                await context.SaveChangesAsync();

                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "OTP is incorrect."
                };
            }

            // Đúng OTP → cập nhật password
            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == token.UserId);
            if (user is null)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "User not found."
                };

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            token.Consumed = true;

            await context.SaveChangesAsync();

            return new ResetPasswordResponse(); // Success=true, message mặc định
        }
    }
}