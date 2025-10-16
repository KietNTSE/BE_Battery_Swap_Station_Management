using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Service.Interfaces;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Service.Implementations
{
    public class EmailService(IConfiguration config, ILogger<EmailService> logger) : IEmailService
    {
        private readonly string _mailHost = config["Email:SmtpHost"] ?? "smtp.gmail.com";
        private readonly int _mailPort = int.Parse(config["Email:SmtpPort"] ?? "25");
        private readonly string _mailUser = config["Email:User"] ?? "";
        private readonly string _mailPass = config["Email:Password"] ?? "";
        private readonly bool _mailEnableSsl = bool.Parse(config["Email:EnableSsl"] ?? "false");
        
        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            try
            {
                using var client = await CreateClientAsync();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("EV Driver Dev Team", _mailUser));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlBody
                };
                message.Body = builder.ToMessageBody();

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send email to {To}", to);
                throw;
            }
        }
        
        
        private async Task<SmtpClient> CreateClientAsync()
        {
            var client = new SmtpClient();
            var socketOption = _mailEnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

            await client.ConnectAsync(_mailHost, _mailPort, socketOption);
            await client.AuthenticateAsync(_mailUser, _mailPass);

            return client;
        }
    }
}