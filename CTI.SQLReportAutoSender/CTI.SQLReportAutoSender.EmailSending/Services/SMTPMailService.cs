using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace CTI.SQLReportAutoSender.EmailSending.Services
{
    public class SMTPEmailService : IEmailSender
    {
        private readonly MailSettings _settings;
        public SMTPEmailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(_settings.SMTPEmail!, email, subject, message)
            {
                IsBodyHtml = true
            };
            using var client = new SmtpClient();
            client.Host = _settings.SMTPHost!;
            client.Port = _settings.SMTPPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(_settings.SMTPEmail!, _settings.SMTPEmailPassword);
            await client.SendMailAsync(mailMessage);
        }
    }
}
