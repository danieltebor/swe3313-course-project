namespace HanksMineralEmporium.Data.DatabaseIO.Json.Tests;

internal class MockDatabaseObject : IDatabaseObject
{
    public ulong Id { get; }
    public string Name { get; set; } = "MockDatabaseObject";

    public MockDatabaseObject(ulong id)
    {
        Id = id;
    }
}