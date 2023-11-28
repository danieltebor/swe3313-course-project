
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
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task RegisterUserAsync(string username, string password)
    {
        var hashedPassword = PasswordHashUtil.HashPassword(password);
        await _userManager.RegisterUserAsync(username, hashedPassword);
    }

    public async Task LoginUserAsync(string username, string password)
    {
        var user = await _userManager.LoadUserAsync(username);

        if (!PasswordHashUtil.VerifyPassword(password, user.Password))
        {
            throw new PasswordMismatchException("Incorrect Password.");
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append("UserId", user.Id.ToString());
        _httpContextAccessor.HttpContext.Response.Cookies.Append("Username", user.Username);
    }

    public async Task LogoutUserAsync(ulong userId)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserId");
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("Username");
    }
}
