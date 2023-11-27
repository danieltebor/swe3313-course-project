using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.UserManagement.Exception;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Factory implementation for creating and retrieving users.
/// </summary>
internal class UserFactory : IUserFactory
{
    private readonly IUserDatabaseOperator _userDatabaseOperator;

    public UserFactory(IUserDatabaseOperator userDatabaseOperator) {
        _userDatabaseOperator = userDatabaseOperator 
            ?? throw new ArgumentNullException(nameof(userDatabaseOperator));
    }

    /// <inheritdoc/>
    public IUser CreateNewUser(string username, string password)
    {
        CredentialValidation.ValidateUsername(username);
        CredentialValidation.ValidatePassword(password);

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