using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Domain.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashedByte = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedByte).Replace("-","").ToLower();
                return hash;
            }
        }
    }
}
