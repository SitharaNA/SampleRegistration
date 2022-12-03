using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Domain
{
    public static class PasswordHash
    {
        public static string Hash(this string data)
        {
            byte[] salt = new byte[128 / 8];

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: data!,
                            salt: salt,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8));
        }
    }
}
