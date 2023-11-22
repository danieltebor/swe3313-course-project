using Newtonsoft.Json;

using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.UserManagement;

public interface IUser : IJsonDatabaseObject
{
    [JsonProperty("username")]
    public string Username { get; }
    [JsonProperty("password")]
    public string Password { get; }
    [JsonProperty("isAdmin")]
    public bool IsAdmin { get; }
}