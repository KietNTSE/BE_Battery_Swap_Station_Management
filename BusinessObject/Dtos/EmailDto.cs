namespace BusinessObject.Dtos;

public class WelcomeEmailModel
{
    public string Name { get; set; }
    public string Link { get; set; }
}

public class PasswordResetOtpModel
{
    public string FullName { get; set; } = string.Empty;
    public string Otp { get; set; } = string.Empty;
}