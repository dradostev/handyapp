using System;
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
    }
}