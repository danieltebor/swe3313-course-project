namespace HanksMineralEmporium.Service.AuthenticationService.Exception;
/// <summary>
/// Represents an exception thrown when two passwords do not match during authentication.
/// </summary>
    class PasswordMismatchException : System.Exception
    {
     /// <summary>
    /// Initializes a new instance of the <see cref="PasswordMismatchException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public PasswordMismatchException(string message) : base(message){}

    }
