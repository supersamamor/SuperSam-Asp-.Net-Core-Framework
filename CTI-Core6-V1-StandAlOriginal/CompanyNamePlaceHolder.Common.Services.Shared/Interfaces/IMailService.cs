using CompanyNamePlaceHolder.Common.Services.Shared.Models.Mail;

namespace CompanyNamePlaceHolder.Common.Services.Shared.Interfaces;

/// <summary>
/// Represents an email sending service
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Sends an email
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SendAsync(MailRequest request, CancellationToken cancellationToken = default);
}