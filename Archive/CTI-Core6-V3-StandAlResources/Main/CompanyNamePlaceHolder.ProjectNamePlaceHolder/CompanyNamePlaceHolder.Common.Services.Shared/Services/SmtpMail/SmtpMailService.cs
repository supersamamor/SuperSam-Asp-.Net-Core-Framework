using CompanyNamePlaceHolder.Common.Services.Shared.Interfaces;
using CompanyNamePlaceHolder.Common.Services.Shared.Models.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CompanyNamePlaceHolder.Common.Services.Shared.Services.SmtpMail;

/// <summary>
/// The email sending service.
/// </summary>
public class SmtpMailService : IMailService
{
    readonly SmtpSettings SmtpSettings;

    /// <summary>
    /// Creates an instance of <see cref="SmtpMailService"/>
    /// with the specified options.
    /// </summary>
    /// <param name="options"></param>
    public SmtpMailService(IOptions<SmtpSettings> options)
    {
        SmtpSettings = options.Value;
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
        smtp.Connect(SmtpSettings.Host, SmtpSettings.Port, SecureSocketOptions.StartTls, cancellationToken);
        smtp.Authenticate(SmtpSettings.Email, SmtpSettings.Password, cancellationToken);
        await smtp.SendAsync(email, cancellationToken);
        smtp.Disconnect(true, cancellationToken);
    }
}