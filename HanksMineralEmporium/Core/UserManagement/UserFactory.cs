using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.UserManagement.Exception;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Factory implementation for creating and retrieving users.
/// </summary>
internal class UserFactory : IUserFactory
{
    [NotNull]
    private readonly IUserDatabaseOperator _userDatabaseOperator;

    public UserFactory([DisallowNull] IUserDatabaseOperator userDatabaseOperator) {
        _userDatabaseOperator = userDatabaseOperator 
            ?? throw new ArgumentNullException(nameof(userDatabaseOperator));
    }

    private static void ValidateUsername(string username)
    {
        if (username is null)
        {
            throw new ArgumentNullException(nameof(username));
        }
        else if (username.Length < 3)
        {
            throw new InvalidUsernameException("Username must be at least 3 characters long.");
        }
        else if (username.Length > 32)
        {
            throw new InvalidUsernameException("Username cannot be longer than 32 characters.");
        }
    }

    private static void ValidatePassword(string password)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        else if (password.Length < 8)
        {
            throw new InvalidPasswordException("Password must be at least 8 characters long.");
        }
        else if (password.Length > 72)
        {
            throw new InvalidPasswordException("Password cannot be longer than 72 characters.");
        }
    }

    /// <inheritdoc/>
    public IUser CreateNewUser(string username, string password)
    {
        ValidateUsername(username);
        ValidatePassword(password);

        if (_userDatabaseOperator.IsUsernameTakenAsync(username).Result) {
            throw new InvalidUsernameException("Username is already taken.");
        }

        var user = new User(_userDatabaseOperator.GetNewUniqueId(), username, password);
        _userDatabaseOperator.SaveAsync(user).Wait();

        return user;
    }

    /// <inheritdoc/>
    public IUser GetUserById(ulong id)
    {
        var user = _userDatabaseOperator.GetByIdAsync(id).Result
            ?? throw new UserNotFoundException(id);
        return user;
    }

    /// <inheritdoc/>
    public IUser GetUserByUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));
        }

        var user = _userDatabaseOperator.GetByUsernameAsync(username).Result
            ?? throw new UserNotFoundException(username);
        return user;
    }
}