using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class EmailService(IConfiguration config, ILogger<EmailService> logger) : IEmailService
    {
        public async Task SendAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
        {
            var section = config.GetSection("Email");
            var from = section["From"];
            var host = section["SmtpHost"];
            var port = int.TryParse(section["SmtpPort"], out var p) ? p : 587;
            var enableSsl = bool.TryParse(section["EnableSsl"], out var ssl) && ssl;
            var user = section["User"];
            var pass = section["Password"];

            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(from))
            {
                
                logger.LogInformation("Email (fallback log). To: {to}, Subject: {subject}, Body: {body}", to, subject, htmlBody);
                return;
            }

            using var client = new SmtpClient(host, port)
            {
                Credentials = string.IsNullOrWhiteSpace(user) ? CredentialCache.DefaultNetworkCredentials : new NetworkCredential(user, pass),
                EnableSsl = enableSsl
            };
            using var mail = new MailMessage(from!, to, subject, htmlBody) { IsBodyHtml = true };

            await client.SendMailAsync(mail, ct);
        }
    }
}