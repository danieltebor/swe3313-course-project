using System.Reflection;

using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Data.DatabaseIO.Json.Tests;

[Collection("Database tests")]
public class JsonSalesDatabaseOperatorTests
{
    private readonly string _databasePath = Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            ?? throw new NullReferenceException(),
        "Resources",
        "Database",
        "Sales.json");
    
    [Fact]
    public void GetSalesByReceiptIdAsync_ExistingReceipt_ReturnsSales()
    {
        // Arrange.
        var salesDatabaseOperator = new JsonSalesDatabaseOperator();

        var sale1 = new Sale(salesDatabaseOperator.GetNewUniqueId(), 0, 0);
        var sale2 = new Sale(salesDatabaseOperator.GetNewUniqueId(), 1, 0);

        salesDatabaseOperator.SaveAsync(sale1).Wait();
        salesDatabaseOperator.SaveAsync(sale2).Wait();

        // Act.
        var result = salesDatabaseOperator.GetSalesByReceiptIdAsync(0).Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetSalesByReceiptIdAsync_NonExistingReceipt_ReturnsEmptyList()
    {
        // Arrange.
        var salesDatabaseOperator = new JsonSalesDatabaseOperator();

        var sale1 = new Sale(salesDatabaseOperator.GetNewUniqueId(), 0, 0);
        var sale2 = new Sale(salesDatabaseOperator.GetNewUniqueId(), 1, 0);

        salesDatabaseOperator.SaveAsync(sale1).Wait();
        salesDatabaseOperator.SaveAsync(sale2).Wait();

        // Act.
        var result = salesDatabaseOperator.GetSalesByReceiptIdAsync(1).Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetSoldItemIdsAsync_ExistingSales_ReturnsItemIds()
    {
        // Arrange.
        var salesDatabaseOperator = new JsonSalesDatabaseOperator();

        var sale1 = new Sale(salesDatabaseOperator.GetNewUniqueId(), 0, 0);
        var sale2 = new Sale(salesDatabaseOperator.GetNewUniqueId(), 1, 0);

        salesDatabaseOperator.SaveAsync(sale1).Wait();
        salesDatabaseOperator.SaveAsync(sale2).Wait();

        // Act.
        var result = salesDatabaseOperator.GetSoldItemIdsAsync().Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Contains(0ul, result);
        Assert.Contains(1ul, result);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetSoldItemIdsAsync_NoSales_ReturnsEmptyList()
    {
        // Arrange.
        var salesDatabaseOperator = new JsonSalesDatabaseOperator();

        // Act.
        var result = salesDatabaseOperator.GetSoldItemIdsAsync().Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result);

        // Cleanup.
        File.Delete(_databasePath);
    }
}