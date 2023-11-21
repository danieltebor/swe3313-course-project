using Newtonsoft.Json;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Base definition for a database object.
/// </summary>
public interface IJsonDatabaseObject
{
    [JsonProperty("id")]
    public ulong Id { get; }
}