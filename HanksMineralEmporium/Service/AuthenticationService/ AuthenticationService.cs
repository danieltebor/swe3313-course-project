using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Core.UserManagement.Exception;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using Microsoft.AspNetCore.Http.HttpResults;
using HanksMineralEmporium.Service.AuthenticationService.Exception;


namespace HanksMineralEmporium.Service.AuthenticationService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private IUserManager _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(IUserManager userManager, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task RegisterUserAsync(string username, string password)
        {
            // Create user and validate
            var user = await _userManager.LoadUserAsync(username);
            _userManager.RegisterUserAsync(username, password);
        }

        public async Task LoginUserAsync(string username, string password)
        {
            _ = await _userManager.LoadUserAsync(username);
            IUser? user = await _userManager.RegisterUserAsync(username, password);
            
            // hash password
            var hashedPassword = Shared.Util.PasswordHashUtil.HashPassword(password);

            if (user.Password.Equals(hashedPassword))
            {
                StoreUserInSession(user);
            }
            else
            {
                throw new PasswordMismatchException("Incorrect Password");
            }
        }

        public async Task LogoutUserAsync(ulong userId)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("Username");
        }

        private void StoreUserInSession(IUser u)
        {
            // Create a cookie with the session data
            // Store user information in the session as a cookie
            var user = _userManager.LoadUserAsync(u.Username);
            var userId = user.Id;
            
            // Use IUserManager to get the user account name
            var accountName = u.Username;

            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", userId.ToString());
            _httpContextAccessor.HttpContext.Response.Cookies.Append("Username", accountName);
        }
    }
}
