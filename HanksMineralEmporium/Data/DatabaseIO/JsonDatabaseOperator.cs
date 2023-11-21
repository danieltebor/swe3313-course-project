using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Provides base implementation for a database operator that uses JSON files.
/// </summary>
internal abstract class JsonDatabaseOperator : IDatabaseOperator<IJsonDatabaseObject>
{
    [NotNull]
    private readonly string _databasePath;
    [NotNull]
    private ISet<ulong> _transientIds = new HashSet<ulong>();
    [NotNull]
    private ulong _lastId = 0;
    
    [NotNull]
    protected Mutex _databaseLock = new();
    [NotNull]
    protected JsonSerializerSettings _serializerSettings;

    /// <summary>
    /// Creates a new JsonDatabaseOperator.
    /// </summary>
    /// <param name="databasePath"></param>
    /// <exception cref="ArgumentException">Thrown when database path is null or empty.</exception>
    public JsonDatabaseOperator(string databasePath)
    {
        if (string.IsNullOrWhiteSpace(databasePath))
        {
            throw new ArgumentException("Database path cannot be null or whitespace.", nameof(databasePath));
        }

        _databasePath = databasePath;

        _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };
    }

    /// <inheritdoc/>
    public void Save(IJsonDatabaseObject obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        _databaseLock.WaitOne();
        try
        {
            var jsonFile = File.ReadAllText(_databasePath);
            var objects = JsonConvert.DeserializeObject<List<IJsonDatabaseObject>>(jsonFile, _serializerSettings)
                ?? new List<IJsonDatabaseObject>();

            if (!_transientIds.Contains(obj.Id))
            {
                throw new ArgumentException("Object ID is not transient. "
                    + "Make sure the id is generated with GenerateNewId.", nameof(obj));
            }
            _transientIds.Remove(obj.Id);

            int idx = objects.FindLastIndex(existingObj => existingObj.Id < obj.Id);
            
            if (idx == -1)
            {
                objects.Insert(0, obj);
            }
            else
            {
                objects.Insert(idx + 1, obj);
            }

            File.WriteAllText(_databasePath, JsonConvert.SerializeObject(objects, _serializerSettings));
        }
        finally
        {
            _databaseLock.ReleaseMutex();
        }
    }

    /// <inheritdoc/>
    public IJsonDatabaseObject? GeyById(ulong id)
    {
        _databaseLock.WaitOne();
        try
        {
            var jsonFile = File.ReadAllText(_databasePath);
            var objects = JsonConvert.DeserializeObject<List<IJsonDatabaseObject>>(jsonFile, _serializerSettings);
            
            if (objects == null || objects.Count == 0)
            {
                return null;
            }

            return objects[(int)id];
        }
        finally
        {
            _databaseLock.ReleaseMutex();
        }
    }

    /// <inheritdoc/>
    public List<IJsonDatabaseObject> GetAll()
    {
        _databaseLock.WaitOne();
        try
        {
            var jsonFile = File.ReadAllText(_databasePath);
            var objects = JsonConvert.DeserializeObject<List<IJsonDatabaseObject>>(jsonFile, _serializerSettings);
            
            return objects ?? new List<IJsonDatabaseObject>();
        }
        finally
        {
            _databaseLock.ReleaseMutex();
        }
    }

    /// <inheritdoc/>
    public ulong GetNewUniqueId()
    {
        _databaseLock.WaitOne();
        try {
            _lastId++;
            _transientIds.Add(_lastId);
            
            return _lastId;
        } finally {
            _databaseLock.ReleaseMutex();
        }
    }
}