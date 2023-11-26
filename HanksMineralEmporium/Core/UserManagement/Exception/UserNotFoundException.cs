namespace HanksMineralEmporium.Core.UserManagement.Exception;

/// <summary>
/// Exception thrown when a user is not found.
/// </summary>
public class UserNotFoundException : System.Exception
{
    /// <summary>
    /// Creates a new <see cref="UserNotFoundException"/>.
    /// </summary>
    /// <param name="id">The ID of the user that was not found.</param>
    public UserNotFoundException(ulong id) : base($"User with ID {id} not found.") {}
    /// <summary>
    /// Creates a new <see cref="UserNotFoundException"/>.
    /// </summary>
    /// <param name="username">The username of the user that was not found.</param>
    public UserNotFoundException(string username) : base($"User with username '{username}' not found.") {}
}