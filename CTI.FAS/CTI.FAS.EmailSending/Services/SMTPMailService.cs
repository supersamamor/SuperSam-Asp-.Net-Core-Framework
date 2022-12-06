using CTI.Common.Services.Shared.Interfaces;
using CTI.Common.Services.Shared.Models.Mail;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace CTI.FAS.EmailSending.Services
{
    public class SMTPEmailService : IMailService
    {
        private readonly MailSettings _settings;
        public SMTPEmailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(MailRequest request, CancellationToken cancellationToken = default)
        {
            var mailMessage = new MailMessage(_settings.SMTPEmail!, request.To, request.Subject, request.Body)
            {
                IsBodyHtml = true,
            };
            if (request.Attachments != null && request.Attachments.Count > 0)
            {
                foreach (var item in request.Attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(item));
                }
            }
            using var client = new SmtpClient();
            client.Host = _settings.SMTPHost!;
            client.Port = _settings.SMTPPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(_settings.SMTPEmail!, _settings.SMTPEmailPassword);
            await client.SendMailAsync(mailMessage, cancellationToken);
        }
    }
}
