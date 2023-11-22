using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace HanksMineralEmporium.Data.DatabaseIO
{
    /// <summary>
    /// Contract for IDatabaseObject serialization.
    /// </summary>
    public interface IDatabaseObjectSerializer<T> where T : IDatabaseObject
    {
        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="obj">The object to be serialized</param>
        /// <returns>The string of the serialized object.</returns>
        /// <exception cref="ArgumentNullException">Thrown when obj is null.</exception>
        public string SerializeObject([DisallowNull] T obj);

        /// <summary>
        /// Serializes a list of objects to a string.
        /// </summary>
        /// <param name="objects">The objects to be serialized</param>
        /// <returns>The string of the serialized objects.</returns>
        /// <exception cref="ArgumentNullException">Thrown when objects is null.</exception>
        public string SerializeList([DisallowNull] IList<T> objects);

        /// <summary>
        /// Deserializes a string to an object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The resulting object from deserializing the object string.</returns>
        /// <exception cref="ArgumentException">Thrown when data is null or whitespace.</exception>
        /// <exception cref="JsonException">Thrown when data is not a valid JSON string.</exception>
        /// <exception cref="InvalidCastException">Thrown when data is not a valid JSON object.</exception>
        public T DeserializeObject([DisallowNull] string data);

        /// <summary>
        /// Deserializes a string to a list of objects.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The resulting object from deserializing the object string.</returns>
        /// <exception cref="ArgumentException">Thrown when data is null or whitespace.</exception>
        /// <exception cref="JsonException">Thrown when data is not a valid JSON string.</exception>
        /// <exception cref="InvalidCastException">Thrown when data is not a valid JSON array.</exception>
        public List<T> DeserializeList([DisallowNull] string data);
    }
}