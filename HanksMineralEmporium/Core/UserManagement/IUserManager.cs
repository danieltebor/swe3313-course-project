using HanksMineralEmporium.Core.UserManagement.Exception;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Contract for managing users.
/// </summary>
public interface IUserManager
{
    /// <summary>
    /// Creates a new user with the given username and password. The id is generated automatically.
    /// </summary>
    /// <param name="username">The username of the user to be created.</param>
    /// <param name="hashedPassword">The password of the user to be created.</param>
    /// <returns>The created user</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or <paramref name="hashedPassword"/> is null or whitespace.</exception>
    /// <exception cref="InvalidUsernameException">Thrown when the given username is invalid or already taken.</exception>
    public Task<IUser> RegisterUserAsync(string username, string hashedPassword);

    /// <summary>
    /// Gets an existing user by their username.
    /// </summary>
    /// <param name="username">The username of the user to load.</param>
    /// <returns>The retreived user.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is null or whitespace.</exception>
    /// <exception cref="UserNotFoundException">Thrown when no user with the given username exists.</exception>
    public Task<IUser> LoadUserAsync(string username);

    /// <summary>
    /// Makes the given user an admin.
    /// </summary>
    /// <param name="userToPromote">The user to promote.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userToPromote"/> or <paramref name="adminPromoting"/> is null.</exception>
    /// <exception cref="UserNotFoundException">Thrown when the user to promote does not exist.</exception>
    public Task MakeAdminAsync(IUser userToPromote);

    /// <summary>
    /// Removes the admin status from the given user.
    /// </summary>
    /// <param name="userToDemote">The user to demote.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userToDemote"/> or <paramref name="adminPromoting"/> is null.</exception>
    /// <exception cref="UserNotFoundException">Thrown when the user to demote does not exist.</exception>
    public Task DemoteAdminAsync(IUser userToDemote);
}