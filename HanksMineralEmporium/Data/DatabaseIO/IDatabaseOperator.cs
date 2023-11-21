namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Base definition for a database operator.
/// </summary>
/// <typeparam name="T">The type of the object to be saved to the database</typeparam>
public interface IDatabaseOperator<T>
{
    /// <summary>
    /// Saves an object to the database.
    /// </summary>
    /// <param name="obj">The object to save.</param>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    /// <exception cref="IOException">Thrown when an IO error occurs while writing to the database file.</exception>
    public void Save(T obj);
    
    /// <summary>
    /// Gets an object from the database by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IJsonDatabaseObject?</returns>
    /// <exception cref="IOException">Thrown when an IO error occurs while reading from the database file.</exception>
    public T? GeyById(ulong id);

    /// <summary>
    /// Gets all objects from the database.
    /// </summary>
    /// <returns>List of all objects in the database.</returns>
    /// <exception cref="IOException">Thrown when an IO error occurs while reading from the database file.</exception>
    public List<T> GetAll();

    // <summary>
    /// Generates a new unique ID for an object.
    /// </summary>
    /// <returns>Unique ID.</returns>
    /// <exception cref="IOException">Thrown when an IO error occurs while reading from the database file.</exception>
    public ulong GetNewUniqueId();
}