namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Base definition for a JSON database object.
/// </summary>
public interface IDatabaseObject
{
    /// <summary>
    /// The unique ID of the object.
    /// </summary>
    public ulong Id { get; }
}