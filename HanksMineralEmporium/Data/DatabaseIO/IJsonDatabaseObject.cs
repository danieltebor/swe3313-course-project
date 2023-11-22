using Newtonsoft.Json;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Base definition for a JSON database object.
/// </summary>
public interface IJsonDatabaseObject : IDatabaseObject
{
    [JsonProperty("id")]
    public new ulong Id { get; }
}