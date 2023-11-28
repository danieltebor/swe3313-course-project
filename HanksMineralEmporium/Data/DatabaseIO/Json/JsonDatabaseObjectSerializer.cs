using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

/// <summary>
/// JSON implementation of <see cref="IDatabaseObjectSerializer{T}"/>.
/// </summary>
internal class JsonDatabaseObjectSerializer<T> : IDatabaseObjectSerializer<T> where T : IDatabaseObject
{
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public JsonDatabaseObjectSerializer() {
        _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
        };
    }

    public string SerializeObject(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
        
        try
        {
            return JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("An error occurred while serializing the object.", ex);
        }
    }

    public T DeserializeObject(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(data));
        }

        try
        {
            var obj = JsonConvert.DeserializeObject<T>(data, _jsonSerializerSettings)
                ?? throw new InvalidCastException("Data is not a valid JSON object.");
            return obj;
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("An error occurred while deserializing the object.", ex);
        }
    }

    public string SerializeList(IList<T> objects)
    {
        if (objects == null)
        {
            throw new ArgumentNullException(nameof(objects));
        }

        try
        {
            return JsonConvert.SerializeObject(objects, _jsonSerializerSettings);
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("An error occurred while serializing the list.", ex);
        }
    }

    public List<T> DeserializeList(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(data));
        }

        try
        {
            var objects = JsonConvert.DeserializeObject<List<T>>(data, _jsonSerializerSettings)
                ?? throw new InvalidCastException("Data is not a valid JSON array.");
            return objects;
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("An error occurred while deserializing the list.", ex);
        }
    }
}