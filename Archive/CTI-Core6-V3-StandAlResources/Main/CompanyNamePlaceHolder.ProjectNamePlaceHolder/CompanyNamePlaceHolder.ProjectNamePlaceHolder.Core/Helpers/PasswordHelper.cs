using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
namespace CompanyNamePlaceHolder.ProjectNamePlaceHolder.Core.Helpers
{
    public static class PasswordHelper
    {
        public static List<string> ValidatePassword(string password)
        {
            List<string> validationMessages = new();
            if (password.Length < 8)
            {
                validationMessages.Add("Password should have a minimum length of 8 characters.");
            }
            if (!password.Any(char.IsUpper))
            {
                validationMessages.Add("Password should contain at least one uppercase letter.");
            }
            if (!password.Any(char.IsLower))
            {
                validationMessages.Add("Password should contain at least one lowercase letter.");
            }
            if (!password.Any(char.IsDigit))
            {
                validationMessages.Add("Password should contain at least one numeric digit.");
            }
            if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=[\]{};':""\\|,.<>/?]"))
            {
                validationMessages.Add("Password should contain at least one special character (!@#$%^&*()_+-=[]{};':\"\\|,.<>/?).");
            }
            if (password.Contains('\''))
            {
                validationMessages.Add("Password should not contain the single quote character (').");
            }
            return validationMessages;
        }
        public static string EncryptPassword(string password, string emailAsKey)
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

        public static string DecryptPassword(string encryptedPassword, string emailAsKey)
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
