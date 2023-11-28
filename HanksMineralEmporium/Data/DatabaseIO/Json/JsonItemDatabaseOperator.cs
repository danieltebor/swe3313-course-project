using HanksMineralEmporium.Core.InventoryManagement;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

internal class JsonItemDatabaseOperator : JsonDatabaseOperator<IItem>, IItemDatabaseOperator
{
    private static readonly string DatabaseName = "Items";

    private void DownloadSeedImages()
    {
        //var url = "";
        //HttpClient client = new();

        // TODO: Download seed images.
    }

    protected override IReadOnlyList<IItem> GetSeedData()
    {
        //TODO: Add seed data.
        List<IItem> seedData = new();
        return seedData;
    }

    public JsonItemDatabaseOperator()
        : base(DatabaseName, new JsonDatabaseObjectSerializer<IItem>())
    {
        DownloadSeedImages();
    }
}