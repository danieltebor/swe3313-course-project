using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Shared.Util;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

/// <summary>
/// JSON implementation of <see cref="IUserDatabaseOperator"/>.
/// </summary>
public class JsonUserDatabaseOperator : JsonDatabaseOperator<IUser>, IUserDatabaseOperator
{
    private static readonly string DatabaseName = "Users";

    private readonly ISet<string> _transientUsernames = new HashSet<string>();

    protected override IReadOnlyList<IUser> GetSeedData()
    {
        List<IUser> seedData = new()
        {
            new User(GetNewUniqueId(), "admin", PasswordHashUtil.HashPassword("admin"), true)
        };

        return seedData;
    }

    public JsonUserDatabaseOperator()
        : base(DatabaseName, new JsonDatabaseObjectSerializer<IUser>()) {}

    /// <inheritdoc/>
    public override async Task SaveAsync(IUser user) {
        await base.SaveAsync(user);

        await _databaseLock.WaitAsync();
        try
        {
            _transientUsernames.Remove(user.Username);
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    private IUser? GetByUsernameHelper(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));
        }

        var jsonFileStr = File.ReadAllText(_databasePath);
        var objects = _jsonSerializer.DeserializeList(jsonFileStr);
            
        if (objects == null || objects.Count == 0)
        {
            return default;
        }

        var user = objects.FirstOrDefault(u => u.Username == username);
        return user;
    }

    /// <inheritdoc/>
    public async Task<IUser?> GetByUsernameAsync(string username)
    {
        await _databaseLock.WaitAsync();
        try
        {
            return GetByUsernameHelper(username);
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <inheritdoc/>
    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        await _databaseLock.WaitAsync();
        try
        {
            var user = GetByUsernameHelper(username);
            var userIsTaken = user != null || _transientUsernames.Contains(username);

            if (!userIsTaken)
            {
                _transientUsernames.Add(username);
            }

            return userIsTaken;
        }
        finally
        {
            _databaseLock.Release();
        }
    }
}