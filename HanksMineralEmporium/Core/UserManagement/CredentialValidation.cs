using HanksMineralEmporium.Core.UserManagement.Exception;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Static class for validating credentials.
/// </summary>
internal static class CredentialValidation
{
    public static void ValidateUsername(string username)
    {
        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }
        else if (username.Length < 3)
        {
            throw new InvalidUsernameException("Username must be at least 3 characters long.");
        }
        else if (username.Length > 32)
        {
            throw new InvalidUsernameException("Username cannot be longer than 32 characters.");
        }
    }

    public static void ValidatePassword(string password)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        else if (password.Length < 8)
        {
            throw new InvalidPasswordException("Password must be at least 8 characters long.");
        }
        else if (password.Length > 72)
        {
            throw new InvalidPasswordException("Password cannot be longer than 72 characters.");
        }
    }
}