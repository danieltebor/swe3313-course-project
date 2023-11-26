using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.UserManagement.Exception;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Factory for creating and retrieving users.
/// </summary>
internal interface IUserFactory
{
    /// <summary>
    /// Creates a new user with the given username and password. The id is generated automatically 
    /// and the user is saved in the database.
    /// </summary>
    /// <param name="username">The username of the user to create.</param>
    /// <param name="password">The password of the user to create.</param>
    /// <returns>The created IUser.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> or <paramref name="password"/> is null or whitespace.</exception>
    /// <exception cref="InvalidUsernameException">Thrown when the given username is invalid or already taken.</exception>
    /// <exception cref="InvalidPasswordException">Thrown when the given password is invalid.</exception>
    public IUser CreateNewUser([DisallowNull] string username, [DisallowNull] string password);
    
    /// <summary>
    /// Gets an existing user by their username.
    /// </summary>
    /// <param name="id">The id of the user to load.</param>
    /// <returns>The retreived IUser</returns>
    /// <exception cref="UserNotFoundException">Thrown when no user with the given ID exists.</exception>
    public IUser GetUserById(ulong id);

    /// <summary>
    /// Gets an existing user by their username.
    /// </summary>
    /// <param name="username">The username of the user to load.</param>
    /// <returns>The retreived IUser</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="username"/> is null or whitespace.</exception>
    /// <exception cref="UserNotFoundException">Thrown when no user with the given username exists.</exception>
    public IUser GetUserByUsername([DisallowNull] string username);
}