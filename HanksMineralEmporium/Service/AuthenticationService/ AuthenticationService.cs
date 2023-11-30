
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Service.AuthenticationService.Exception;
using HanksMineralEmporium.Shared.Util;

namespace HanksMineralEmporium.Service.AuthenticationService;

/// <summary>
/// Initializes a new instance of the <see cref="AuthenticationService"/> class.
/// </summary>
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserManager _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(IUserManager userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <inheritdoc/>
    public async Task RegisterUserAsync(string username, string password)
    {
        CredentialValidation.ValidateUsername(username);
        CredentialValidation.ValidatePassword(password);

        var hashedPassword = PasswordHashUtil.HashPassword(password);
        await _userManager.RegisterUserAsync(username, hashedPassword);
    }

    /// <inheritdoc/>
    public async Task LoginUserAsync(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new System.Exception("Invalid Username.");
        }
        else if (string.IsNullOrWhiteSpace(password))
        {
            throw new System.Exception("Invalid Password.");
        }

        var user = await _userManager.LoadUserAsync(username);

        if (!PasswordHashUtil.VerifyPassword(password, user.Password))
        {
            throw new PasswordMismatchException("Incorrect Password.");
        }

        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");
        httpContext.Session.SetString("UserId", user.Id.ToString());
        httpContext.Session.SetString("Username", user.Username);
        httpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
    }

    /// <inheritdoc/>
    public void LogoutUser()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");
        httpContext.Session.Remove("UserId");
        httpContext.Session.Remove("Username");
        httpContext.Session.Remove("IsAdmin");
    }
}
