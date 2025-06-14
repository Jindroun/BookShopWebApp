namespace DataAccessLayer.Utilities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

public static class HashingHelper
{
    public static string HashPassword(string password, byte[] salt, int iterations = 100000, int hashByteSize = 32)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            iterations,
            hashByteSize
        ));
    }

    public static bool VerifyPassword(string password, string hash, byte[] salt)
    {
        var passwordHash = HashPassword(password, salt);
        return passwordHash.SequenceEqual(hash);
    }

    public static byte[] GenerateSalt(int byteSize = 16)
    {
        return RandomNumberGenerator.GetBytes(byteSize);
    }
}
