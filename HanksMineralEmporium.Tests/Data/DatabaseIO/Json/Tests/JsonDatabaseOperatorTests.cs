using System.Reflection;

using HanksMineralEmporium.Data.DatabaseIO.Exception;

namespace HanksMineralEmporium.Data.DatabaseIO.Json.Tests;

internal class TestJsonDatabaseOperator : JsonDatabaseOperator<IDatabaseObject>
{
    protected override IReadOnlyList<IDatabaseObject> GetSeedData()
    {
        var seedData = new List<IDatabaseObject>();
        var id1 = GetNewUniqueId();
        var id2 = GetNewUniqueId();
        var id3 = GetNewUniqueId();
        seedData.Add(new MockDatabaseObject(id1));
        seedData.Add(new MockDatabaseObject(id2));
        seedData.Add(new MockDatabaseObject(id3));
        return seedData;
    }

    public TestJsonDatabaseOperator()
        : base("Generic", new JsonDatabaseObjectSerializer<IDatabaseObject>()) {}
}

[Collection("Database tests")]
public class JsonDatabaseOperatorTests
{
    private readonly string _databasePath = Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ?? throw new NullReferenceException(),
        "Resources",
        "Database",
        "Generic.json");

    [Fact]
    public void InitializeDatabase_WithExistingDatabaseSmallerThanSeedData_DoesNotOverwrite()
    {
        // Arrange.
        var seedData = new List<IDatabaseObject>();
        seedData.Add(new MockDatabaseObject(0));
        seedData.Add(new MockDatabaseObject(1));

        var seedDataJson = new JsonDatabaseObjectSerializer<IDatabaseObject>().SerializeList(seedData);

        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);
        File.WriteAllText(_databasePath, seedDataJson);

        // Act.
        var databaseOperator = new TestJsonDatabaseOperator();
        var result = File.ReadAllText(_databasePath);

        // Assert.
        Assert.Equal(seedDataJson, result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void InitializeDatabase_WithExistingDatabaseSameSizeAsSeedData_DoesNotOverwrite()
    {
        // Arrange.
        var seedData = new List<IDatabaseObject>();
        seedData.Add(new MockDatabaseObject(0));
        seedData.Add(new MockDatabaseObject(1));
        seedData.Add(new MockDatabaseObject(2));

        var seedDataJson = new JsonDatabaseObjectSerializer<IDatabaseObject>().SerializeList(seedData);

        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);
        File.WriteAllText(_databasePath, seedDataJson);

        // Act.
        var databaseOperator = new TestJsonDatabaseOperator();
        var result = File.ReadAllText(_databasePath);

        // Assert.
        Assert.Equal(seedDataJson, result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void InitializeDatabase_WithExistingDatabaseLargerThanSeedData_DoesNotOverwrite()
    {
        // Arrange.
        var seedData = new List<IDatabaseObject>();
        seedData.Add(new MockDatabaseObject(0));
        seedData.Add(new MockDatabaseObject(1));
        seedData.Add(new MockDatabaseObject(2));
        seedData.Add(new MockDatabaseObject(3));

        var seedDataJson = new JsonDatabaseObjectSerializer<IDatabaseObject>().SerializeList(seedData);

        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);
        File.WriteAllText(_databasePath, seedDataJson);

        // Act.
        var databaseOperator = new TestJsonDatabaseOperator();
        var result = File.ReadAllText(_databasePath);

        // Assert.
        Assert.Equal(seedDataJson, result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void InitializeDatabase_WithEmptyFile_CreatesDatabaseWithSeedData()
    {
        // Arrange.
        var seedData = new List<IDatabaseObject>();
        seedData.Add(new MockDatabaseObject(0));
        seedData.Add(new MockDatabaseObject(1));
        seedData.Add(new MockDatabaseObject(2));

        var seedDataJson = new JsonDatabaseObjectSerializer<IDatabaseObject>().SerializeList(seedData);

        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);
        File.WriteAllText(_databasePath, "");

        // Act.
        var databaseOperator = new TestJsonDatabaseOperator();
        var result = File.ReadAllText(_databasePath);

        // Assert.
        Assert.Equal(seedDataJson, result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void InitializeDatabase_WithNoExistingDatabaseFile_CreatesDatabaseWithSeedData()
    {
        // Arrange.
        var seedData = new List<IDatabaseObject>();
        seedData.Add(new MockDatabaseObject(0));
        seedData.Add(new MockDatabaseObject(1));
        seedData.Add(new MockDatabaseObject(2));

        var seedDataJson = new JsonDatabaseObjectSerializer<IDatabaseObject>().SerializeList(seedData);

        Directory.CreateDirectory(Path.GetDirectoryName(_databasePath)!);

        // Act.
        var databaseOperator = new TestJsonDatabaseOperator();
        var result = File.ReadAllText(_databasePath);

        // Assert.
        Assert.Equal(seedDataJson, result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void InitializeDatabase_WithNoExistingDatabaseDirectory_CreatesDatabaseWithSeedData()
    {
        // Arrange.
        var seedData = new List<IDatabaseObject>();
        seedData.Add(new MockDatabaseObject(0));
        seedData.Add(new MockDatabaseObject(1));
        seedData.Add(new MockDatabaseObject(2));

        var seedDataJson = new JsonDatabaseObjectSerializer<IDatabaseObject>().SerializeList(seedData);

        if (Directory.Exists(Path.GetDirectoryName(_databasePath)!))
        {
            Directory.Delete(Path.GetDirectoryName(_databasePath)!, true);
        }

        // Act.
        var databaseOperator = new TestJsonDatabaseOperator();
        var result = "";
        try
        {
            result = File.ReadAllText(_databasePath);
        }
        catch (DirectoryNotFoundException)
        {
            // Assert.
            Assert.False(true);
        }
        finally
        {
            // Cleanup.
            File.Delete(_databasePath);
        }

        // Assert.
        Assert.Equal(seedDataJson, result);
    }

    [Fact]
    public void SaveAsync_OutOfOrder_AddsObjectsToDatabaseInCorrectOrder()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();
        var databaseObject1 = new MockDatabaseObject(databaseOperator.GetNewUniqueId());
        var databaseObject2 = new MockDatabaseObject(databaseOperator.GetNewUniqueId());
        var databaseObject3 = new MockDatabaseObject(databaseOperator.GetNewUniqueId());
        var objects1 = new List<IDatabaseObject>();
        objects1.Add(new MockDatabaseObject(0));
        objects1.Add(new MockDatabaseObject(1));
        objects1.Add(new MockDatabaseObject(2));
        objects1.Add(databaseObject1);
        objects1.Add(databaseObject2);
        objects1.Add(databaseObject3);

        // Act.
        databaseOperator.SaveAsync(databaseObject1).Wait();
        databaseOperator.SaveAsync(databaseObject3).Wait();
        databaseOperator.SaveAsync(databaseObject2).Wait();
        var objects2 = databaseOperator.GetAllAsync().Result;

        // Assert.
        for (int i = 0; i < objects1.Count; i++)
        {
            Assert.Equal(objects1[i].Id, objects2[i].Id);
        }

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void SaveAsync_OutOfOrder_DoesNotAddDuplicateObjectsToDatabase()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();
        var databaseObject1 = new MockDatabaseObject(databaseOperator.GetNewUniqueId());
        var databaseObject2 = new MockDatabaseObject(databaseOperator.GetNewUniqueId());
        var databaseObject3 = new MockDatabaseObject(databaseOperator.GetNewUniqueId());
        var objects1 = new List<IDatabaseObject>();
        objects1.Add(new MockDatabaseObject(0));
        objects1.Add(new MockDatabaseObject(1));
        objects1.Add(new MockDatabaseObject(2));
        objects1.Add(databaseObject1);
        objects1.Add(databaseObject2);
        objects1.Add(databaseObject3);

        // Act.
        databaseOperator.SaveAsync(databaseObject1).Wait();
        databaseOperator.SaveAsync(databaseObject3).Wait();
        databaseOperator.SaveAsync(databaseObject2).Wait();
    
        var resultTask = Assert.ThrowsAsync<InvalidIdException>(() => databaseOperator.SaveAsync(databaseObject1));
        resultTask.Wait();
        var result = resultTask.Result;
        var objects2 = databaseOperator.GetAllAsync().Result;

        // Assert.
        for (int i = 0; i < objects1.Count; i++)
        {
            Assert.Equal(objects1[i].Id, objects2[i].Id);
        }
        Assert.Equal("Object ID is not transient. "
                    + "Make sure the id is generated with GenerateNewUniqueId().", result.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void SaveAsync_NullObject_ThrowsArgumentNullException()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentNullException>(() => databaseOperator.SaveAsync(null!));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'obj')", result.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void SaveAsync_ObjectWithoutTransientId_ThrowsInvalidIdException()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();
        var databaseObject = new MockDatabaseObject(3);

        // Act.
        var resultTask = Assert.ThrowsAsync<InvalidIdException>(() => databaseOperator.SaveAsync(databaseObject));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Object ID is not transient. "
                    + "Make sure the id is generated with GenerateNewUniqueId().", result.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void OverwriteAsync_ExistingObject_OverwritesExistingObject()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();
        var newObject = new MockDatabaseObject(0);
        newObject.Name = "NewName";

        // Act.
        databaseOperator.OverwriteAsync(newObject).Wait();
        var obj = databaseOperator.GetByIdAsync(0).Result;

        // Assert.
        Assert.Equal(newObject.Name, ((MockDatabaseObject)obj!).Name);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void OverwriteAsync_NonExistingObject_ThrowsDatabaseObjectNotFoundException()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();
        var newObject = new MockDatabaseObject(3);
        newObject.Name = "NewName";

        // Act.
        var resultTask = Assert.ThrowsAsync<DatabaseObjectNotFoundException<IDatabaseObject>>(() => databaseOperator.OverwriteAsync(newObject));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Object of type IDatabaseObject with ID 3 was not found in the database.", result.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void OverwriteAsync_NullObject_ThrowsArgumentNullException()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();

        // Act.
        var resultTask = Assert.ThrowsAsync<ArgumentNullException>(() => databaseOperator.OverwriteAsync(null!));
        resultTask.Wait();
        var result = resultTask.Result;

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'obj')", result.Message);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetByIdAsync_ExistingObject_ReturnsObject()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();

        // Act.
        var result = databaseOperator.GetByIdAsync(0).Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Equal((ulong)0, result.Id);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetByIdAsync_NonExistingObject_ReturnsNull()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();

        // Act.
        var result = databaseOperator.GetByIdAsync(3).Result;

        // Assert.
        Assert.Null(result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetAllAsync_ReturnsAllObjects()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();
        var objects1 = new List<IDatabaseObject>();
        objects1.Add(new MockDatabaseObject(0));
        objects1.Add(new MockDatabaseObject(1));
        objects1.Add(new MockDatabaseObject(2));

        // Act.
        var objects2 = databaseOperator.GetAllAsync().Result;

        // Assert.
        for (int i = 0; i < objects1.Count; i++)
        {
            Assert.Equal(objects1[i].Id, objects2[i].Id);
        }
        Assert.Equal(objects1.Count, objects2.Count);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetNewUniqueId_ReturnsUniqueId()
    {
        // Arrange.
        var databaseOperator = new TestJsonDatabaseOperator();

        // Act.
        var id1 = databaseOperator.GetNewUniqueId();
        var id2 = databaseOperator.GetNewUniqueId();
        var id3 = databaseOperator.GetNewUniqueId();

        // Assert.
        Assert.Equal((ulong)3, id1);
        Assert.Equal((ulong)4, id2);
        Assert.Equal((ulong)5, id3);

        // Cleanup.
        File.Delete(_databasePath);
    }
}