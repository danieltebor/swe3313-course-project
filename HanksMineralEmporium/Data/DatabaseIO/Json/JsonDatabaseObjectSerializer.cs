using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

public class JsonDatabaseObjectSerializer<T> : IDatabaseObjectSerializer<T> where T : IDatabaseObject
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

        return JsonConvert.SerializeObject(objects, _jsonSerializerSettings);
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