using System.Diagnostics.CodeAnalysis;

namespace HanksMineralEmporium.Data.DatabaseIO.Exception;

/// <summary>
/// Thrown when an object's ID is invalid.
/// </summary>
class InvalidIdException : System.Exception
{
    /// <summary>
    /// Creates a new InvalidIdException.
    /// </summary>
    /// <param name="message">Message to display.</param>
    public InvalidIdException([DisallowNull] string message) : base(message) {}
}