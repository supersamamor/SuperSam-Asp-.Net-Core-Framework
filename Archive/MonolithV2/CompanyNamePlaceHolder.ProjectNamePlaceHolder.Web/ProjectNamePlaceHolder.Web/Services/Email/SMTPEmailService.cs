using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectNamePlaceHolder.Web.Services.Email
{
    public class SMTPEmailService : IEmailSender
    {
        EmailSettings _settings;
        public SMTPEmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var mailMessage = new MailMessage(_settings.Email, email, subject, message);
            mailMessage.IsBodyHtml = true;
            using (var client = new SmtpClient())
            {
                client.Host = _settings.Host;
                client.Port = _settings.Port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(_settings.Email, _settings.Password);
                await client.SendMailAsync(mailMessage);
            }
        }
    }   
}
