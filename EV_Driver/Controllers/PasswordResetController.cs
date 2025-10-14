using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class PasswordResetController(IPasswordResetService passwordResetService) : ControllerBase
    {
        // Gửi OTP quên mật khẩu
        [HttpPost("forgot")]
        public async Task<ActionResult<ResponseObject<object>>> Forgot([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await passwordResetService.RequestPasswordResetAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "If the email exists, an OTP has been sent.",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        // Xác nhận OTP và đặt lại mật khẩu
        [HttpPost("reset")]
        public async Task<ActionResult<ResponseObject<object>>> Reset([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await passwordResetService.ResetPasswordAsync(request);
            return Ok(new ResponseObject<object>
            {
                Message = "Password has been reset successfully.",
                Code = "200",
                Success = true,
                Content = null
            });
        }
    }
}