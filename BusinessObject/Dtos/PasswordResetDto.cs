using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Dtos
{
    public class ForgotPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }

    public class ResetPasswordRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(6, MinimumLength = 6)]
        public string Otp { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
