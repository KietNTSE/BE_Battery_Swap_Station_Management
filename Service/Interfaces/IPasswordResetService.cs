using BusinessObject.Dtos;
using static BusinessObject.Dtos.ForgotPasswordResponse;

namespace Service.Interfaces
{
    public interface IPasswordResetService
    {
        Task<ForgotPasswordResponse> RequestPasswordResetAsync(ForgotPasswordRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}