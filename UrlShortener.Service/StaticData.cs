using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Service
{
    public static class StaticData
    {
        public const string ShortUrlTemplate = "Shtnr";
        public const string RoleAdmin = "Admin";
        public const string RoleCustomer = "Customer";

        public static string DecryptString(string encryptedText)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decrypted = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.Unicode.GetString(decrypted);
        }

        public static string PasswordHashCoder(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] encrypted = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encrypted);
        }

        public static string CreateShortUrl()
        {
            Random random = new Random();
            int RandomValue = random.Next(101, 10001);
            return  ShortUrlTemplate + "\\" +RandomValue.ToString();
        }
    }
}
