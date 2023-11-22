using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

/// <summary>
/// Provides base implementations for operations on a JSON database.
/// </summary>
public abstract class JsonDatabaseOperator<T> : IDatabaseOperator<T> where T : IDatabaseObject
{
    [NotNull]
    private readonly ISet<ulong> _transientIds = new HashSet<ulong>();
    [NotNull]
    private ulong _lastId = 0;
    
    [NotNull]
    protected readonly string _databasePath = Path.Combine(Environment.CurrentDirectory, "Data", "Database") 
                                                  + Path.DirectorySeparatorChar;
    [NotNull]
    protected readonly IDatabaseObjectSerializer<T> _jsonSerializer;
    [NotNull]
    protected readonly SemaphoreSlim _databaseLock = new(1, 1);

    protected abstract IReadOnlyList<T> GetSeedData();

    private void InitializeDatabase()
    {
        if (File.Exists(_databasePath))
        {
            if (GetAll().Result.Count > 0)
            {
                return;
            }
        }

        var seedData = GetSeedData();

        _databaseLock.Wait();
        try
        {
            var directoryPath = Path.GetDirectoryName(_databasePath);
            if (!Directory.Exists(directoryPath!))
            {           
                Directory.CreateDirectory(directoryPath!);
            }

            File.WriteAllText(_databasePath, _jsonSerializer.SerializeList((IList<T>)seedData));
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <summary>
    /// Creates a new JsonDatabaseOperator.
    /// </summary>
    /// <param name="databaseName">Name of the JSON database file. Should not end in .json.</param>
    /// <exception cref="ArgumentException">Thrown when database path is null or empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when jsonSerializer is null.</exception>
    public JsonDatabaseOperator([DisallowNull] string databaseName, [DisallowNull] IDatabaseObjectSerializer<T> jsonSerializer)
    {
        if (string.IsNullOrWhiteSpace(databaseName))
        {
            throw new ArgumentException("Database name cannot be null or whitespace.", nameof(databaseName));
        }
        else if (jsonSerializer == null)
        {
            throw new ArgumentNullException(nameof(jsonSerializer));
        }

        _databasePath = _databasePath + databaseName + ".json";
        _jsonSerializer = jsonSerializer;

        InitializeDatabase();
    }

    /// <inheritdoc/>
    public async Task Save(T obj)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }

        await _databaseLock.WaitAsync();
        try
        {
            var jsonFile = File.ReadAllText(_databasePath);
            var objects = _jsonSerializer.DeserializeList(jsonFile);

            if (!_transientIds.Contains(obj.Id))
            {
                throw new ArgumentException("Object ID is not transient. "
                    + "Make sure the id is generated with GenerateNewId.", nameof(obj));
            }
            _transientIds.Remove(obj.Id);

            int idx = objects!.FindLastIndex(existingObj => existingObj.Id < obj.Id);
            
            if (idx == -1)
            {
                objects.Insert(0, obj);
            }
            else
            {
                objects.Insert(idx + 1, obj);
            }

            File.WriteAllText(_databasePath, _jsonSerializer.SerializeList(objects));
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <inheritdoc/>
    public async Task<T?> GeyById(ulong id)
    {
        await _databaseLock.WaitAsync();
        try
        {
            var jsonFileStr = File.ReadAllText(_databasePath);
            var objects = _jsonSerializer.DeserializeList(jsonFileStr);
            
            if (objects == null || objects.Count == 0)
            {
                return default;
            }

            return objects[(int)id];
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<T>> GetAll()
    {
        await _databaseLock.WaitAsync();
        try
        {
            var jsonFile = File.ReadAllText(_databasePath);
            var objects = _jsonSerializer.DeserializeList(jsonFile);
            
            return objects ?? new List<T>();
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <inheritdoc/>
    public ulong GetNewUniqueId()
    {
        _databaseLock.Wait();
        try {
            _lastId++;
            _transientIds.Add(_lastId);
            
            return _lastId;
        } finally {
            _databaseLock.Release();
        }
    }
}