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
        : base(DatabaseName, (IDatabaseObjectSerializer<IUser>)new JsonUserSerializer()) {}

    /// <inheritdoc/>
    public Task<IUser?> GetByUsername(string username)
    {
        
    }

    /// <inheritdoc/>
    public Task<bool> IsUsernameTaken(string username)
    {
        
    }

    /// <inheritdoc/>
    public Task MakeAdmin(ulong userId)
    {
        
    }

    /// <inheritdoc/>
    public Task MakeAdmin(IUser user)
    {
        
    }

    /// <inheritdoc/>
    public Task MakeAdmin(string username)
    {
        
    }
}