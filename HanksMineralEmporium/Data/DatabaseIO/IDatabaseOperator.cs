using HanksMineralEmporium.Data.DatabaseIO.Exception;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for a database operator that handles <see cref="IDatabaseObject"/> objects.
/// </summary>
/// <typeparam name="T">The type of the object to be saved to the database</typeparam>
internal interface IDatabaseOperator<T> where T : IDatabaseObject
{
    /// <summary>
    /// Saves an object to the database.
    /// </summary>
    /// <param name="obj">The object to save.</param>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    /// <exception cref="InvalidIdException">Thrown when obj has an ID that is already in use.</exception>
    /// <exception cref="IOException">Thrown when an IO error occurs while writing to the database file.</exception>
    public Task SaveAsync(T obj);

    /// <summary>
    /// Overwrites the object with the same object that is passed in.
    /// </summary>
    /// <param name="obj">The object to overwrite.</param>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    /// <exception cref="DatabaseObjectNotFoundException{T}">Thrown when obj does not exist in the database.</exception>
    /// <exception cref="IOException">Thrown when an IO error occurs while writing to the database file.</exception>
    public Task OverwriteAsync(T obj);
    
    /// <summary>
    /// Gets an object from the database by its ID.
    /// </summary>
    /// <param name="id">The id of the object to get.</param>
    /// <returns>The object with the given ID, or null if no object with that ID exists.</returns>
    /// <exception cref="IOException">Thrown when an IO error occurs while reading from the database file.</exception>
    public Task<T?> GetByIdAsync(ulong id);

    /// <summary>
    /// Gets all objects from the database.
    /// </summary>
    /// <returns>List of all objects in the database.</returns>
    /// <exception cref="IOException">Thrown when an IO error occurs while reading from the database file.</exception>
    public Task<IReadOnlyList<T>> GetAllAsync();

    /// <summary>
    /// Generates a new unique ID for an object.
    /// </summary>
    /// <returns>Unique ID.</returns>
    /// <exception cref="IOException">Thrown when an IO error occurs while reading from the database file.</exception>
    public ulong GetNewUniqueId();
}