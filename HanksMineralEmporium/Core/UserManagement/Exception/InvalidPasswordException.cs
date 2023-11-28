namespace HanksMineralEmporium.Core.UserManagement.Exception;

/// <summary>
/// Exception thrown when an invalid password is given.
/// </summary>
public class InvalidPasswordException : ArgumentException
{
    /// <summary>
    /// Creates a new <see cref="InvalidPasswordException"/>.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <exception cref="ArgumentNullException">Thrown when message is null.</exception>
    public InvalidPasswordException(string message) : base(message) {
        if (message is null)
        {
            throw new ArgumentNullException(nameof(message));
        }
    }
}