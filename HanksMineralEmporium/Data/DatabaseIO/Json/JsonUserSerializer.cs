using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json.Serialization;

using HanksMineralEmporium.Core.UserManagement;
using System.Security.Cryptography.X509Certificates;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

class JsonUserSerializer : IDatabaseObjectSerializer<IUser>
{
    [NotNull]
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public JsonUserSerializer() {
        _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
        };
    }

    public string SerializeObject(IUser obj)
    {
        return JsonConvert.SerializeObject(obj, _jsonSerializerSettings);
    }

    public string SerializeList(IList<IUser> objects)
    {
        return JsonConvert.SerializeObject(objects, _jsonSerializerSettings);
    }

    public IUser DeserializeObject(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data cannot be null or whitespace.", nameof(data));
        }

        var user = JsonConvert.DeserializeObject<IUser>(data, _jsonSerializerSettings)
            ?? throw new InvalidCastException("Data is not a valid JSON object.");
        return user;
    }

    public List<IUser> DeserializeList(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            throw new ArgumentException("Data cannot be null or whitespace.", nameof(data));
        }

        return JsonConvert.DeserializeObject<List<IUser>>(data, _jsonSerializerSettings)
            ?? throw new InvalidCastException("Data is not a valid JSON array.");
    }
}