using CTI.Common.Services.Shared.Interfaces;
using CTI.Common.Services.Shared.Models.Mail;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace CTI.DSF.EmailSending.Services
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
            if (!string.IsNullOrEmpty(_settings.TestEmailRecipient))
            {
                string[] testEmails = _settings.TestEmailRecipient.Split(',');
                request.To = testEmails[0];
                request.Ccs = testEmails.ToList();
                request.Bcc = testEmails.ToList();
                request.Subject += " - Test";
            }
            var decryptedPassword = DecryptPassword(_settings.SMTPEmailPassword!, _settings.SMTPEmail!);
            var mailMessage = new MailMessage(_settings.SMTPEmail!, request.To, request.Subject, request.Body)
            {
                IsBodyHtml = true
            };
            using var client = new SmtpClient();
            client.Host = _settings.SMTPHost!;
            client.Port = _settings.SMTPPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(_settings.SMTPEmail!, decryptedPassword);
            await client.SendMailAsync(mailMessage, cancellationToken);
        }  
        private static string EncryptPassword(string password, string emailAsKey)
        {
            if (string.IsNullOrEmpty(password)) { return ""; }
            byte[] encryptedBytes;
            var key = DeriveKeyFromEmail(emailAsKey);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = new byte[16]; // Initialization Vector
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                encryptedBytes = encryptor.TransformFinalBlock(passwordBytes, 0, passwordBytes.Length);
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        private static string DecryptPassword(string encryptedPassword, string emailAsKey)
        {

            if (string.IsNullOrEmpty(encryptedPassword)) { return ""; }
            byte[] decryptedBytes;
            var key = DeriveKeyFromEmail(emailAsKey);
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = new byte[16]; // Initialization Vector
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedPassword);
                decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }

 

        private static byte[] DeriveKeyFromEmail(string email)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] emailBytes = Encoding.UTF8.GetBytes(email);
                return sha256.ComputeHash(emailBytes);
            }
        }
    }
}
