using CompanyNamePlaceHolder.Common.Services.Shared.Interfaces;
using CompanyNamePlaceHolder.Common.Services.Shared.Models.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyNamePlaceHolder.Common.Services.Shared.Services.SmtpMail
{
    public class SmtpMailService : IMailService
    {
        readonly SmtpSettings SmtpSettings;

        public SmtpMailService(IOptions<SmtpSettings> options)
        {
            SmtpSettings = options.Value;
        }

        public async Task SendAsync(MailRequest request, CancellationToken cancellationToken = default)
        {
            var builder = new BodyBuilder
            {
                HtmlBody = request.Body
            };
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(SmtpSettings.Email),
                Subject = request.Subject,
                Body = builder.ToMessageBody()
            };
            email.To.Add(MailboxAddress.Parse(request.To));
            using var smtp = new SmtpClient();
            smtp.Connect(SmtpSettings.Host, SmtpSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(SmtpSettings.Email, SmtpSettings.Password);
            await smtp.SendAsync(email, cancellationToken);
            smtp.Disconnect(true);
        }
    }
}