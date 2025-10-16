using System.Net;
using BusinessObject;
using BusinessObject.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class PasswordResetService(ApplicationDbContext context, IEmailService emailService, IEmailTemplateLoaderService emailTemplateLoaderService, IDistributedCache cache)
        : IPasswordResetService
    {
        private static string GenerateOtp()
        {
            var rng = Random.Shared.Next(100000, 999999);
            return rng.ToString();
        }

        public async Task RequestPasswordResetAsync(ForgotPasswordRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
            {
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Email not found."
                };
            }

            var otp = GenerateOtp();
            var otpKey = "reset_otp" + otp;
            var otpCheck = false;
            do
            {
                var checkOtp = await cache.GetStringAsync(otpKey);
                if (checkOtp == otp)
                {
                    otp = GenerateOtp();
                    otpKey = "reset_otp" + otp;
                }
                else
                {
                    otpCheck = true;
                }
            } while (!otpCheck);
            
            var cacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
            };
            
            await cache.SetStringAsync(otpKey,user.UserId, cacheEntryOptions);

            const string subject = "EV Driver - OTP code to reset password";

            try
            {
                var loader = await emailTemplateLoaderService.RenderTemplateAsync("ResetPassword.cshtml",
                    new PasswordResetOtpModel
                    {
                        FullName = user.FullName,
                        Otp = otp,
                    });

                await emailService.SendEmailAsync(user.Email, subject, loader);
            }
            catch (Exception ex)
            {
                await cache.RemoveAsync(otpKey);
                throw new ValidationException
                {
                    ErrorMessage = ex.Message,
                    Code = "500",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "NewPassword and ConfirmPassword do not match."
                };
            
            var otpKey = "reset_otp" + request.Otp;
            
            var userId = await cache.GetStringAsync(otpKey);
            if (userId is null)
            {
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "Invalid OTP."
                };
            }
            
            var userEntity = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (userEntity is null)
            {
                throw new ValidationException
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Code = "400",
                    ErrorMessage = "User not found."
                };
            }

            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ValidationException
                {
                    ErrorMessage = ex.Message,
                    Code = "500",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}