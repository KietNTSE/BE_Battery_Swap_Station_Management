using BusinessObject.Dtos;

namespace Service.Interfaces
{
    public interface IPasswordResetService
    {
        Task RequestPasswordResetAsync(ForgotPasswordRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}