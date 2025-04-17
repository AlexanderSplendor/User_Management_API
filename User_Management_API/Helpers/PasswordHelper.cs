using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;

namespace User_Management_API.Helpers
{
    public class PasswordHelper
    {
        public static bool IsStrongPassword(string password)
        {
            // Minimum 8 characters, at least 1 special character
            return password.Length >= 8 && Regex.IsMatch(password, @"[\W_]");
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
