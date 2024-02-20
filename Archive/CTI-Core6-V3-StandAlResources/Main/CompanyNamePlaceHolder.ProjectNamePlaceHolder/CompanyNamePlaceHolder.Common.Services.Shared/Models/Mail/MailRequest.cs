namespace CompanyNamePlaceHolder.Common.Services.Shared.Models.Mail;

/// <summary>
/// The email request.
/// </summary>
public class MailRequest
{
    /// <summary>
    /// Recipient of the email.
    /// </summary>
    public string To { get; set; } = "";

    /// <summary>
    /// Subject of the email.
    /// </summary>
    public string Subject { get; set; } = "";

    /// <summary>
    /// The body of the email.
    /// </summary>
    public string Body { get; set; } = "";
	public IList<string>? Attachments { get; set; }
    public IList<string>? Ccs { get; set; }
    public IList<string>? Bcc { get; set; }
}