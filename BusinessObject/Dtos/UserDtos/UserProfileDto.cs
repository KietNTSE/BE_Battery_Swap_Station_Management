using BusinessObject.Enums;

namespace BusinessObject.Dtos.UserDtos
{
    public class UserProfileDto
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
