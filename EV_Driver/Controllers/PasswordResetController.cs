using BusinessObject.Dtos;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Service.Exceptions;
using Service.Interfaces;
using System.Net;
namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordResetController(IPasswordResetService service) : ControllerBase
    {
        [HttpPost("forgot")]
        public async Task<ActionResult<ResponseObject<object>>> Forgot([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new ResponseObject<object>{ Success = false, Message = "Invalid request", Errors = errors });
            }

            await service.RequestPasswordResetAsync(request);
            return Ok(new ResponseObject<object>{
                Message = "Send OTP to email",
                Code = "200",
                Success = true,
                Content = null
            });
        }

        [HttpPost("reset")]
        public async Task<ActionResult<ResponseObject<object>>> Reset([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Success = false, Message = "Invalid request", Errors = errors });
            }

            try
            {
                await service.ResetPasswordAsync(request);
                return Ok(new { Success = true, Message = "Password reset successful." });
            }
            catch (ValidationException ex)
            {
                return StatusCode((int)ex.StatusCode, new { Success = false, Code = ex.Code, Message = ex.ErrorMessage });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Success = false, Message = ex.Message });
            }
        }
    }
}