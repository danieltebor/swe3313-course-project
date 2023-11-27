namespace HanksMineralEmporium.Core.UserManagement.Exception;

/// <summary>
/// Exception thrown when an invalid username is given.
/// </summary>
public class InvalidUsernameException : ArgumentException
{
    /// <summary>
    /// Creates a new <see cref="InvalidUsernameException"/>.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <exception cref="ArgumentNullException">Thrown when message is null.</exception>
    public InvalidUsernameException(string message) : base(message) {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }
    }
}