using HanksMineralEmporium.Core.UserManagement;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for a database operator that handles <see cref="IUser"/> objects.
/// </summary>
public interface IUserDatabaseOperator : IDatabaseOperator<IUser>
{
    /// <summary>
    /// Checks if a username is taken. Registers the username as transient if it is not.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <returns>True if the username is taken, false otherwise.</returns>
    public Task<bool> IsUsernameTaken(string username);

    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    /// <returns>The user with the given username, or null if no user with the given username exists.</returns>
    public Task<IUser> GetByUsername(string username);

    /// <summary>
    /// Converts an existing user to an admin.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    public Task MakeAdmin(ulong userId);
    /// <summary>
    /// Converts an existing user to an admin.
    /// </summary>
    /// <param name="username">The username of the user.</param>
    public Task MakeAdmin(string username);
    /// <summary>
    /// Converts an existing user to an admin.
    /// </summary>
    /// <param name="user">A corresponding IUser object.</param>
    public Task MakeAdmin(IUser user);
}