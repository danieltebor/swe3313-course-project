using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Shared.Util;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// JSON implementation for a user database operator.
/// </summary>
public class JsonUserDatabaseOperator : JsonDatabaseOperator, IUserDatabaseOperator<IJsonDatabaseObject>
{
    private static readonly string DatabaseName = "Users";

    private readonly ISet<string> _transientUsernames = new HashSet<string>();

    protected override IReadOnlyList<IJsonDatabaseObject> GetSeedData()
    {
        List<IJsonDatabaseObject> seedData = new()
        {
            new User(GetNewUniqueId(), "admin", PasswordHashUtil.HashPassword("admin"), true)
        };

        return seedData;
    }

    public JsonUserDatabaseOperator() : base(DatabaseName) {}

    /// <inheritdoc/>
    public Task<IJsonDatabaseObject?> GetByUsername(string username)
    {
        
    }

    /// <inheritdoc/>
    public Task<bool> IsUsernameTaken(string username)
    {
        
    }
}