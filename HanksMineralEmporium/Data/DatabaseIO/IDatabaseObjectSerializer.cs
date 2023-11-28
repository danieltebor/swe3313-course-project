using Newtonsoft.Json;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for IDatabaseObject serialization.
/// </summary>
internal interface IDatabaseObjectSerializer<T> where T : IDatabaseObject
{
    /// <summary>
    /// Serializes an object to a string.
    /// </summary>
    /// <param name="obj">The object to be serialized</param>
    /// <returns>The string of the serialized object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while serializing the object.</exception>
    public string SerializeObject(T obj);

    /// <summary>
    /// Deserializes a string to an object.
    /// </summary>
    /// <param name="data"></param>
    /// <returns>The resulting object from deserializing the object string.</returns>
    /// <exception cref="ArgumentException">Thrown when data is null or whitespace.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while deserializing the object.</exception>
    public T DeserializeObject(string data);

    /// <summary>
    /// Serializes a list of objects to a string.
    /// </summary>
    /// <param name="objects">The objects to be serialized</param>
    /// <returns>The string of the serialized objects.</returns>
    /// <exception cref="ArgumentNullException">Thrown when objects is null.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while serializing the objects.</exception>
    public string SerializeList(IList<T> objects);

    /// <summary>
    /// Deserializes a string to a list of objects.
    /// </summary>
    /// <param name="data"></param>
    /// <returns>The resulting object from deserializing the object string.</returns>
    /// <exception cref="ArgumentException">Thrown when data is null or whitespace.</exception>
    /// <exception cref="Exception">Thrown when an error occurs while deserializing the object.</exception>
    public List<T> DeserializeList(string data);
}