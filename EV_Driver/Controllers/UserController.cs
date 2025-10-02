using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController(IUserService userService): ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMeUserProfile()
    {
        try
        {
            var user = await userService.GetMeProfileAsync();
            return Ok(new ResponseObject<UserProfileResponse>
            {
                Content = user,
                Message = "User profile retrieved successfully",
                Code = "200",
                Success = true
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseObject<UserProfileResponse>()
            {
                Content = null,
                Message = ex.Message,
                Code = "400",
                Success = false,
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProfileById(string id)
    {
        var user = await userService.GetUserProfileAsync(id);
        if (user is null)
        {
            return NotFound(new ResponseObject<UserProfileResponse>
            {
                Content = null,
                Message = "User not found",
                Code = "404",
                Success = false,
            });
        }
        return Ok(new ResponseObject<UserProfileResponse>
        {
            Content = user,
            Message = "User profile retrieved successfully",
            Code = "200",
            Success = true
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UserProfileRequest request)
    {
        try
        {
            var user = await userService.UpdateUserProfileResponse(id, request);
            if (user is null)
            {
                return NotFound(new ResponseObject<UserProfileResponse>
                {
                    Content = null,
                    Message = "User not found",
                    Code = "404",
                    Success = false,
                });
            }
            return Ok(new ResponseObject<UserProfileResponse>
            {
                Content = user,
                Message = "User profile updated successfully",
                Code = "200",
                Success = true
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseObject<UserProfileResponse>()
            {
                Content = null,
                Message = ex.Message,
                Code = "400",
                Success = false,
            });
        }
    }
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMeProfile([FromBody] UserProfileRequest request)
    {
        try
        {
            var user = await userService.UpdateMeProfileAsync(request);
            if (user is null)
            {
                return NotFound(new ResponseObject<UserProfileResponse>
                {
                    Content = null,
                    Message = "User not found",
                    Code = "404",
                    Success = false,
                });
            }
            return Ok(new ResponseObject<UserProfileResponse>
            {
                Content = user,
                Message = "User profile updated successfully",
                Code = "200",
                Success = true
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseObject<UserProfileResponse>()
            {
                Content = null,
                Message = ex.Message,
                Code = "400",
                Success = false,
            });
        }
    }
}