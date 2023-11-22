namespace HanksMineralEmporium.Data.DatabaseIO
{
    /// <summary>
    /// Contract for IDatabaseObject serialization
    /// </summary>
    public interface IDatabaseObjectSerializer<T> where T : IDatabaseObject
    {
        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The string of the serialized object.</returns>
        public string Serialize(T obj);

        /// <summary>
        /// Deserializes a string to an object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The resulting object from deserializing the object string.</returns>
        public T Deserialize(string data);
    }
}