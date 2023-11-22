namespace HanksMineralEmporium.Core.UserManagement.Exception;

/// <summary>
/// Exception thrown when a user is not found.
/// </summary>
class UserNotFoundException : Exception
{
    /// <summary>
    /// Creates a new <see cref="UserNotFoundException"/>.
    /// </summary>
    /// <param name="id">The ID of the user that was not found.</param>
    public UserNotFoundException(ulong id) : base($"User with ID {id} not found.") {}
}