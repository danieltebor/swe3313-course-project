namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for objects that can be stored in a database.
/// Implementing classes or interfaces should have properties
/// that correspond with fields in a database.
/// </summary>
public interface IDatabaseObject
{
    /// <summary>
    /// Primary Key.
    /// </summary>
    public ulong Id { get; }
}