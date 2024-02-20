namespace CTI.Common.Services.Shared.Services.SmtpMail;

/// <summary>
/// SMTP settings to be used by the email service.
/// </summary>
public class SmtpSettings
{
    /// <summary>
    /// SMTP host
    /// </summary>
    public string Host { get; set; } = "";

    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; set; } = 587;

    /// <summary>
    /// Account to log in to SMTP server
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Password to log in to SMTP server
    /// </summary>
    public string Password { get; set; } = "";
}