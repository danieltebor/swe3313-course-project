using HanksMineralEmporium.Core.UserManagement;

namespace HanksMineralEmporium.Service.AuthenticationService
{
    /// <summary>
    /// Defines the contract for an authentication service.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Attempts to log in a user with the specified username and password.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        public Task LoginUserAsync(string username, string password);
        public Task LogoutUserAsync(ulong userId);

        /// <summary>
        /// Registers a new user with the specified username and password.
        /// </summary>
        /// <param name="username">The desired username for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        public Task RegisterUser(string username, string password);
        
    }
}
