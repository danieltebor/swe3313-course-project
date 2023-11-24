using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Data.DatabaseIO.Exception;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

/// <summary>
/// Provides base implementations for operations on a JSON database.
/// </summary>
public abstract class JsonDatabaseOperator<T> : IDatabaseOperator<T> where T : IDatabaseObject
{
    [NotNull]
    private readonly ISet<ulong> _transientIds = new HashSet<ulong>();
    [NotNull]
    private ulong _currentId = 0;
    
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
            if (GetAllAsync().Result.Count > 0)
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
    public JsonDatabaseOperator([DisallowNull] string databaseName, [DisallowNull] JsonDatabaseObjectSerializer<T> jsonSerializer)
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
    virtual public async Task SaveAsync(T obj)
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
                throw new InvalidIdException("Object ID is not transient. "
                    + "Make sure the id is generated with GenerateNewUniqueId().");
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
    public async Task OverwriteAsync(T obj)
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

            int idx = objects!.FindIndex(existingObj => existingObj.Id == obj.Id);
            if (idx == -1)
            {
                throw new DatabaseObjectNotFoundException<T>(obj.Id);
            }

            objects[idx] = obj;

            File.WriteAllText(_databasePath, _jsonSerializer.SerializeList(objects));
        }
        finally
        {
            _databaseLock.Release();
        }
    }

    /// <inheritdoc/>
    public async Task<T?> GetByIdAsync(ulong id)
    {
        await _databaseLock.WaitAsync();
        try
        {
            var jsonFileStr = File.ReadAllText(_databasePath);
            var objects = _jsonSerializer.DeserializeList(jsonFileStr);
            
            if (objects == null || objects.Count == 0 || objects.Count <= (int)id)
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
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        await _databaseLock.WaitAsync();
        try
        {
            var jsonFile = File.ReadAllText(_databasePath);
            if (string.IsNullOrWhiteSpace(jsonFile))
            {
                return new List<T>();
            }
            var objects = _jsonSerializer.DeserializeList(jsonFile);
            return objects;
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
            _transientIds.Add(_currentId);
            
            return _currentId++;
        } finally {
            _databaseLock.Release();
        }
    }
}