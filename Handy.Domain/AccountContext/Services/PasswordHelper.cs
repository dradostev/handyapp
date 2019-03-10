using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Handy.Domain.AccountContext.Services
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            string hash;
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }

            return hash;
        }
        
        public static string GetRandomString(int length)
        {
            return new string(
                Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
                    .Select(s => s[(new Random()).Next(s.Length)]).ToArray()
                );
        }
    }
}