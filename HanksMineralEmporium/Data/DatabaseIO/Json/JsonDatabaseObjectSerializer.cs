using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

class JsonDatabaseObjectSerializer<T> : IDatabaseObjectSerializer<T> where T : IDatabaseObject
{
    [NotNull]
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
        
        return JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
    }

    public string SerializeList(IList<T> objects)
    {
        if (objects == null)
        {
            throw new ArgumentNullException(nameof(objects));
        }

        return JsonConvert.SerializeObject(objects, _jsonSerializerSettings);
    }

    public T DeserializeObject(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data cannot be null or whitespace.", nameof(data));
        }

        var user = JsonConvert.DeserializeObject<T>(data, _jsonSerializerSettings)
            ?? throw new InvalidCastException("Data is not a valid JSON object.");
        return user;
    }

    public List<T> DeserializeList(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data cannot be null or whitespace.", nameof(data));
        }

        return JsonConvert.DeserializeObject<List<T>>(data, _jsonSerializerSettings)
            ?? throw new InvalidCastException("Data is not a valid JSON array.");
    }
}