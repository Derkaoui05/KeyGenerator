using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KeyGenerator.Utilities
{
    public class LicenseManager
    {
         //⚠️ Ce secret doit être IDENTIQUE dans ErpApp
        private const string SECRET = "sdboemployee";
        public static string GenerateKey(string macAdress)
        {
            var raw = macAdress.ToUpper().Replace(":", "").Replace("-", "");
                 using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(SECRET));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(raw + SECRET));
            var hex = BitConverter.ToString(hash).Replace("-", "");

            // Format : XXXXX-XXXXX-XXXXX-XXXXX
            return string.Join("-", Enumerable.Range(0, 4)
                .Select(i => hex.Substring(i * 5, 5)))
                .ToUpper();
        }
    }
}
