using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Implementation of <see cref="IUserManager"/>.
/// </summary>
internal class UserManager : IUserManager
{
    private readonly IUserFactory _userFactory;
    private readonly IUserDatabaseOperator _userDatabaseOperator;

    public UserManager(IUserDatabaseOperator userDatabaseOperator) {
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
        if (userToPromote is null)
        {
            throw new ArgumentNullException(nameof(userToPromote));
        }

        var user = await LoadUserAsync(userToPromote.Username);
        user.IsAdmin = true;
        await _userDatabaseOperator.OverwriteAsync(user);
    }

    /// <inheritdoc/>
    public async Task DemoteAdminAsync(IUser userToDemote)
    {
        if (userToDemote is null)
        {
            throw new ArgumentNullException(nameof(userToDemote));
        }

        var user = await LoadUserAsync(userToDemote.Username);
        user.IsAdmin = false;
        await _userDatabaseOperator.OverwriteAsync(user);
    }
}