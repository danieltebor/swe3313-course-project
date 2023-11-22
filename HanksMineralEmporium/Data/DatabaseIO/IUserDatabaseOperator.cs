namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Database operator that handles users.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IUserDatabaseOperator<T> : IDatabaseOperator<T>
{
    /// <summary>
    /// Checks if a username is taken. Registers the username as transient if it is not.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>True if the username is taken, false otherwise.</returns>
    public Task<bool> IsUsernameTaken(string username);

    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username"></param>
    /// <returns>The user with the given username, or null if no user with the given username exists.</returns>
    public Task<T?> GetByUsername(string username);
}