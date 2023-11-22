using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Implementation of <see cref="IUserManager"/>.
/// </summary>
public class UserManager : IUserManager
{
    [NotNull]
    private readonly IUserFactory _userFactory;
    [NotNull]
    private readonly IUserDatabaseOperator _userDatabaseOperator;

    public UserManager([DisallowNull] IUserDatabaseOperator userDatabaseOperator) {
        _userDatabaseOperator = userDatabaseOperator 
            ?? throw new ArgumentNullException(nameof(userDatabaseOperator));

        _userFactory = new UserFactory(userDatabaseOperator);
    }

    /// <inheritdoc/>
    public async Task<IUser> RegisterUserAsync(string username, string password)
    {
        return await Task.Run(() => _userFactory.CreateNewUser(username, password));
    }

    /// <inheritdoc/>
    public async Task<IUser> LoadUserAsync(string username)
    {
        return await Task.Run(() => _userFactory.GetUserByUsername(username));
    }

    /// <inheritdoc/>
    public async Task MakeAdminAsync(IUser userToPromote)
    {
        var user = await LoadUserAsync(userToPromote.Username);
        user.IsAdmin = true;
        await _userDatabaseOperator.OverwriteAsync(user);
    }

    /// <inheritdoc/>
    public async Task DemoteAdminAsync(IUser userToDemote)
    {
        var user = await LoadUserAsync(userToDemote.Username);
        user.IsAdmin = false;
        await _userDatabaseOperator.OverwriteAsync(user);
    }
}