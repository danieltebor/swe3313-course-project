namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Factory for creating and retrieving users.
/// </summary>
public interface IUserFactory
{
    /// <summary>
    /// Creates a new user with the given username and password. The id is generated automatically 
    /// and the user is saved in the database.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="isAdmin"></param>
    /// <returns>The created IUser.</returns>
    public IUser CreateNewUser(string username, string password, bool isAdmin = false);
    
    /// <summary>
    /// Gets an existing user by their username.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>The retreived IUser</returns>
    /// <exception cref="UserNotFoundException">Thrown when no user with the given ID exists.</exception>
    public IUser GetUserById(ulong id);

    /// <summary>
    /// Gets an existing user by their username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>The retreived IUser</returns>
    /// <exception cref="UserNotFoundException">Thrown when no user with the given username exists.</exception>
    public IUser GetUserByUsername(string username);
}