using System;
using System.Web;
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Core.UserManagement.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using MudBlazor;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using HanksMineralEmporium.Service.AuthenticationService.Exception;
/// <summary>
/// Initializes a new instance of the <see cref="AuthenticationService"/> class.
/// </summary>
/// <param name="userManager">An implementation of <see cref="IUserManager"/> for user management.</param>
/// <param name="serviceProvider">Service provider for dependency injection.</param>
/// <remarks>
/// The <paramref name="userManager"/> parameter should not be null.
/// </remarks>
/// <summary>
/// Validates user credentials and attempts to log in the user.
/// </summary>
/// <param name="username">The username of the user.</param>
/// <param name="password">The password of the user.</param>

namespace HanksMineralEmporium.Service.AuthenticationService;

public class  AuthenticationService : IAuthenticationService
{
    private IUserManager _userManager;
    public AuthenticationService(IUserManager userManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task RegisterUserAsync(string username, string password)
    {
        // Create the user and validate
        var user = await _userManager.LoadUserAsync(username);
        await RegisterUserAsync(user.Username,user.Password);
    }

    public async Task LoginUserAsync(string username, string password)
    {
        var user = await _userManager.LoadUserAsync(username);
        // hash password
        var HashPassword = Shared.Util.PasswordHashUtil.HashPassword(password);
        if(user.Password.Equals(HashPassword))
        {
            StoreUserInSession(user);
        }             
        else{throw new PasswordMismatchException("The provided passwords do not match.");}
    }

    public async Task LogoutUserAsync(ulong userId)
    {
        HttpContext httpContext = new DefaultHttpContext();
        httpContext.Response.Cookies.Delete("UserId");
        httpContext.Response.Cookies.Delete("Username");
    }

    

    private void StoreUserInSession(IUser u)
    {
        // Create a cookie with the session data
        HttpContext httpContext = new DefaultHttpContext();
        // Store user information in the session as a cookie
        var user = _userManager.LoadUserAsync(u.Username);
        var userId = user.Id;
        // Use IUserManager to get the user account name
        var accountName = u.Username;
        
        httpContext.Response.Cookies.Append("UserId", userId.ToString());
        httpContext.Response.Cookies.Append("Username", accountName);
    }
}