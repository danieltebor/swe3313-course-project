namespace HanksMineralEmporium.Data.DatabaseIO.Json.Tests;

public class JsonDatabaseObjectSerializerTests
{
    private readonly JsonDatabaseObjectSerializer<IDatabaseObject> _serializer;

    public JsonDatabaseObjectSerializerTests()
    {
        _serializer = new JsonDatabaseObjectSerializer<IDatabaseObject>();
    }

    [Fact]
    public void SerializeObject_WithIDatabaseObject_ReturnsJsonString()
    {
        // Arrange.
        var obj = new MockDatabaseObject(0);

        // Act.
        var result = _serializer.SerializeObject(obj);

        // Assert.
        Assert.NotNull(result);
    }

    [Fact]
    public void SerializeObject_WithNullObject_ThrowsArgumentNullException()
    {
        // Arrange.
        IDatabaseObject? obj = null;

        // Act.
        #pragma warning disable CS8604 // Possible null reference argument.
        var result = Assert.Throws<ArgumentNullException>(() => _serializer.SerializeObject(obj));
        #pragma warning restore CS8604 // Possible null reference argument.

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'obj')", result.Message);
    }

    [Fact]
    public void DeserializeObject_WithJsonString_ReturnsIDatabaseObject()
    {
        // Arrange.
        var obj = new MockDatabaseObject(0);
        var jsonString = _serializer.SerializeObject(obj);

        // Act.
        var result = _serializer.DeserializeObject(jsonString);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(obj.Id, result.Id);
    }

    [Fact]
    public void DeserializeObject_WithNullAndEmptyString_ThrowsArgumentException()
    {
        // Arrange.
        string? jsonString1 = null;
        string? jsonString2 = "";

        // Act.
        #pragma warning disable CS8604 // Possible null reference argument.
        var result1 = Assert.Throws<ArgumentException>(() => _serializer.DeserializeObject(jsonString1));
        #pragma warning restore CS8604 // Possible null reference argument.
        var result2 = Assert.Throws<ArgumentException>(() => _serializer.DeserializeObject(jsonString2));

        // Assert.
        Assert.Equal("Value cannot be null or whitespace. (Parameter 'data')", result1.Message);
        Assert.Equal("Value cannot be null or whitespace. (Parameter 'data')", result2.Message);
    }

    [Fact]
    public void DeserializeObject_WithInvalidJsonString_ThrowsException()
    {
        // Arrange.
        var jsonString = "This is not a valid JSON string.";

        // Act.
        var result = Assert.Throws<System.Exception>(() => _serializer.DeserializeObject(jsonString));

        // Assert.
        Assert.Equal("An error occurred while deserializing the object.", result.Message);
    }

    [Fact]
    public void SerializeList_WithListOfIDatabaseObjects_ReturnsJsonString()
    {
        // Arrange.
        var obj1 = new MockDatabaseObject(0);
        var obj2 = new MockDatabaseObject(1);
        var obj3 = new MockDatabaseObject(2);
        var objects = new List<IDatabaseObject> { obj1, obj2, obj3 };

        // Act.
        var result = _serializer.SerializeList(objects);

        // Assert.
        Assert.NotNull(result);
    }

    [Fact]
    public void SerializeList_WithNullList_ThrowsArgumentNullException()
    {
        // Arrange.
        List<IDatabaseObject>? objects = null;

        // Act.
        #pragma warning disable CS8604 // Possible null reference argument.
        var result = Assert.Throws<ArgumentNullException>(() => _serializer.SerializeList(objects));
        #pragma warning restore CS8604 // Possible null reference argument.

        // Assert.
        Assert.Equal("Value cannot be null. (Parameter 'objects')", result.Message);
    }

    [Fact]
    public void DeserializeList_WithJsonString_ReturnsListOfIDatabaseObjects()
    {
        // Arrange.
        var obj1 = new MockDatabaseObject(0);
        var obj2 = new MockDatabaseObject(1);
        var obj3 = new MockDatabaseObject(2);
        var objects = new List<IDatabaseObject> { obj1, obj2, obj3 };
        var jsonString = _serializer.SerializeList(objects);

        // Act.
        var result = _serializer.DeserializeList(jsonString);

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(objects.Count, result.Count);
        Assert.Equal(objects[0].Id, result[0].Id);
        Assert.Equal(objects[1].Id, result[1].Id);
        Assert.Equal(objects[2].Id, result[2].Id);
    }

    [Fact]
    public void DeserializeList_WithNullAndEmptyString_ThrowsArgumentException()
    {
        // Arrange.
        string? jsonString1 = null;
        string? jsonString2 = "";

        // Act.
        #pragma warning disable CS8604 // Possible null reference argument.
        var result1 = Assert.Throws<ArgumentException>(() => _serializer.DeserializeList(jsonString1));
        #pragma warning restore CS8604 // Possible null reference argument.
        var result2 = Assert.Throws<ArgumentException>(() => _serializer.DeserializeList(jsonString2));

        // Assert.
        Assert.Equal("Value cannot be null or whitespace. (Parameter 'data')", result1.Message);
        Assert.Equal("Value cannot be null or whitespace. (Parameter 'data')", result2.Message);
    }

    [Fact]
    public void DeserializeList_WithInvalidJsonString_ThrowsException()
    {
        // Arrange.
        var jsonString = "This is not a valid JSON string.";

        // Act.
        var result = Assert.Throws<System.Exception>(() => _serializer.DeserializeList(jsonString));

        // Assert.
        Assert.Equal("An error occurred while deserializing the list.", result.Message);
    }
}