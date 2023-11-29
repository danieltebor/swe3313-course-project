using HanksMineralEmporium.Core.UserManagement.Exception;
using HanksMineralEmporium.Service.AuthenticationService.Exception;

namespace HanksMineralEmporium.Service.AuthenticationService
{
    /// <summary>
    /// Defines the contract for an authentication service.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registers a new user with the specified username and password.
        /// </summary>
        /// <param name="username">The desired username for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <exception cref="InvalidUsernameException">Thrown when the username is invalid/taken.</exception>
        /// <exception cref="InvalidPasswordException">Thrown when the password is invalid.</exception>
        public Task RegisterUserAsync(string username, string password);

        /// <summary>
        /// Attempts to log in a user with the specified username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <exception cref="PasswordMismatchException">Thrown when the password does not match the user's password.</exception>
        /// <exception cref="UserNotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
        public Task LoginUserAsync(string username, string password);

        /// <summary>
        /// Logs out the current user.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
        public void LogoutUser();
    }
}
