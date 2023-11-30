using BC = BCrypt.Net.BCrypt;

namespace HanksMineralEmporium.Shared.Util;

public static class PasswordHashUtil
{
    public static string HashPassword(string password)
    {
        return BC.EnhancedHashPassword(password);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BC.EnhancedVerify(password, hash);
    }
}