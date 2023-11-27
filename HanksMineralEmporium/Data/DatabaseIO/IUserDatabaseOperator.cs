using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.UserManagement;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for a database operator that handles <see cref="IUser"/> objects.
/// </summary>
internal interface IUserDatabaseOperator : IDatabaseOperator<IUser>
{
    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <returns>The user with the given username, or null if no user with the given username exists.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="username"/> is null or whitespace.</exception>
    public Task<IUser?> GetByUsernameAsync(string username);

    /// <summary>
    /// Checks if a username is taken. Registers the username as transient if it is not.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <returns>True if the username is taken, false otherwise.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="username"/> is null or whitespace.</exception>
    public Task<bool> IsUsernameTakenAsync(string username);
}