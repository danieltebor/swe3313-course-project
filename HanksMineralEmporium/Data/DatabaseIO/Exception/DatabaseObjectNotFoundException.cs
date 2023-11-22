namespace HanksMineralEmporium.Data.DatabaseIO.Exception;

/// <summary>
/// Thrown when an object is not found in the database.
/// </summary>
/// <typeparam name="T">The type of the object that was not found.</typeparam>
public class DatabaseObjectNotFoundException<T> : System.Exception where T : IDatabaseObject
{
    /// <summary>
    /// Creates a new <see cref="DatabaseObjectNotFoundException{T}"/>.
    /// </summary>
    /// <param name="id">The ID of the object that was not found.</param>
    public DatabaseObjectNotFoundException(ulong id) : base($"Object of type {typeof(T).Name} with ID {id} was not found in the database.") {}
}