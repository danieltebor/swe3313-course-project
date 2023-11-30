using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using HanksMineralEmporium.Core.InventoryManagement;

namespace HanksMineralEmporium.Data.DatabaseIO.Json;

internal class JsonItemDatabaseOperator : JsonDatabaseOperator<IItem>, IItemDatabaseOperator
{
    private static readonly string DatabaseName = "Items";

    private class GitHubFile
    {
        public GitHubFile(string name, string? downloadUrl)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }

            Name = name;
            DownloadUrl = downloadUrl;
        }

        public string Name { get; set; }
        public string? DownloadUrl { get; set; }
    }

    private void PopulateSeedImages()
    {
        var seedImagesPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "..",
            "assets",
            "seed-mineral-images");

        var imagesPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "mineral-images");

        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        foreach (var seedImage in Directory.GetFiles(seedImagesPath))
        {
            var seedImageName = Path.GetFileName(seedImage);
            var seedImageSavePath = Path.Combine(imagesPath, seedImageName);

            if (!File.Exists(seedImageSavePath))
            {
                File.Copy(seedImage, seedImageSavePath);
            }
        }
    }

    protected override IReadOnlyList<IItem> GetSeedData()
    {
        List<IItem> seedData = new()
        {
            new Item(GetNewUniqueId(), 8.00m, "Quartz", "200g of quartz.", "quartz.jpg"),
            new Item(GetNewUniqueId(), 8.50m, "Quartz", "210g of quartz.", "quartz.jpg"),
            new Item(GetNewUniqueId(), 7.85m, "Quartz", "180g of quartz.", "quartz.jpg"),
            new Item(GetNewUniqueId(), 10.20m, "Amethyst", "210g of amethyst.", "amethyst.jpg"),
            new Item(GetNewUniqueId(), 9.99m, "Amethyst", "200g of amethyst.", "amethyst.jpg"),
            new Item(GetNewUniqueId(), 5.99m, "Calcite", "180g of calcite.", "calcite.jpg"),
            new Item(GetNewUniqueId(), 6.99m, "Calcite", "230g of calcite.", "calcite.jpg"),
            new Item(GetNewUniqueId(), 11.80m, "Fluorite", "190g of fluorite.", "fluorite.jpg"),
            new Item(GetNewUniqueId(), 12.99m, "Fluorite", "200g of fluorite.", "fluorite.jpg"),
            new Item(GetNewUniqueId(), 9.99m, "Fluorite", "150g of fluorite.", "fluorite.jpg")
        };
        return seedData;
    }

    public JsonItemDatabaseOperator() : base(DatabaseName, new JsonDatabaseObjectSerializer<IItem>())
    {
        Console.WriteLine("Initializing JsonItemDatabaseOperator...");
        PopulateSeedImages();
    }
}