using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

/// <summary>
/// Factory implementation for creating and retrieving users.
/// </summary>
internal class UserFactory : IUserFactory
{
    private readonly IUserDatabaseOperator _userDatabaseOperator;

    public UserFactory([DisallowNull] IUserDatabaseOperator userDatabaseOperator) {
        _userDatabaseOperator = userDatabaseOperator 
            ?? throw new ArgumentNullException(nameof(userDatabaseOperator));
    }

    /// <inheritdoc/>
    public IUser CreateNewUser(string username, string password, bool isAdmin = false)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IUser GetUserById(ulong id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IUser GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }
}