using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.UserManagement.Exception;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Implementation of <see cref="IUser"/>.
/// </summary>
internal class User : IUser
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
                [StringLength(32, MinimumLength = 3)] string username,
                [StringLength(72, MinimumLength = 8)] string hashedPassword,
                bool isAdmin = false)
    {
        CredentialValidation.ValidateUsername(username);

        Id = id;
        Username = username;
        Password = hashedPassword;
        IsAdmin = isAdmin;
    }

    public ulong Id { get; }
    public string Username { get; }
    public string Password { get; }
    public bool IsAdmin { get; set; }
}