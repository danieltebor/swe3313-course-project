using System.Diagnostics.CodeAnalysis;

namespace HanksMineralEmporium.Data.DatabaseIO.Exception;

/// <summary>
/// Thrown when an object's ID is invalid.
/// </summary>
public class InvalidIdException : System.Exception
{
    /// <summary>
    /// Creates a new InvalidIdException.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <exception cref="ArgumentException">Thrown when message is null or whitespace.</exception>
    public InvalidIdException(string message) : base(message) {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(message));
        }
    }
}