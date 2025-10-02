using System.IdentityModel.Tokens.Jwt;
using System.Net;
using BusinessObject;
using BusinessObject.DTOs;
using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations;

public class UserService(ApplicationDbContext context, ILogger<UserService> logger) : IUserService
{
    public async Task<UserProfileResponse?> GetUserProfileAsync(string userId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is not { Status: UserStatus.Active })
        {
            return null;
        }
        return new UserProfileResponse
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            Role = user.Role,
            Status = user.Status,
            CreatedAt = user.CreatedAt
        };
    }
    
    public async Task<UserProfileResponse?> GetMeProfileAsync()
    {
        const string userId = JwtRegisteredClaimNames.Sub;
        if (string.IsNullOrEmpty(userId))
        {
            throw new ValidationException
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Code = "401",
                ErrorMessage = "Unauthorized"
            };
        }
        return await GetUserProfileAsync(userId);
    }

    public async Task<UserProfileResponse?> UpdateUserProfileResponse(string id, UserProfileRequest userProfileDto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user is null)
        {
            return null;
        }
        user.FullName = userProfileDto.FullName;
        user.Email = userProfileDto.Email;
        user.Phone = userProfileDto.Phone;
        try
        {
            await context.SaveChangesAsync();
            return await GetUserProfileAsync(id);
        } catch
        {
            logger.LogError("Error when try to update user profile");
            throw;
        }
    }
    
    public async Task<UserProfileResponse?> UpdateMeProfileAsync(UserProfileRequest userProfile)
    {
        const string userId = JwtRegisteredClaimNames.Sub;
        if (string.IsNullOrEmpty(userId))
        {
            throw new ValidationException
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Code = "401",
                ErrorMessage = "Unauthorized"
            };
        }
        return await UpdateUserProfileResponse(userId, userProfile);
    }

    public async Task UpdatePassword(ChangePasswordRequest request)
    {
        const string id = JwtRegisteredClaimNames.Sub;
        if (string.IsNullOrEmpty(id))
        {
            throw new ValidationException
            {
                Code = "401",
                StatusCode = HttpStatusCode.Unauthorized,
                ErrorMessage = "Unauthorized"
            };
        }
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if (user is null)
        {
            throw new ValidationException
            {
                ErrorMessage = "User not found",
                Code = "400",
                StatusCode = HttpStatusCode.BadRequest,
            };
        }
        
    }
}