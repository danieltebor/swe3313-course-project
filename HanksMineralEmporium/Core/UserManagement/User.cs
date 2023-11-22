using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.UserManagement.Exception;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Implementation of <see cref="IUser"/>.
/// </summary>
public class User : IUser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="id">The user's unique identifier.</param>
    /// <param name="username">The user's username.</param>
    /// <param name="password">The user's password.</param>
    /// <param name="isAdmin">Whether or not the user is an administrator.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="username"/> or <paramref name="password"/> is null.</exception>
    /// <exception cref="InvalidUsernameException">Thrown when <paramref name="username"/> is less than 3 characters or greater than 32 characters.</exception>
    /// <exception cref="InvalidPasswordException">Thrown when <paramref name="password"/> is less than 8 characters or greater than 72 characters.</exception>
    public User(ulong id,
                [DisallowNull, StringLength(32, MinimumLength = 3)] string username,
                [DisallowNull, StringLength(72, MinimumLength = 8)] string password,
                bool isAdmin = false)
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
        else if (password is null)
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

        Id = id;
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
    }

    public ulong Id { get; }
    public string Username { get; }
    public string Password { get; }
    public bool IsAdmin { get; set; }
}