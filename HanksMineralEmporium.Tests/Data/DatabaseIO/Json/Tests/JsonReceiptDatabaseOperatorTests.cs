using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Data.DatabaseIO.Json.Tests;

[Collection("Database tests")]
public class JsonReceiptDatabaseOperatorTests
{
    private readonly string _databasePath = Path.Combine(Environment.CurrentDirectory, "Data", "Database", "Receipts.json");

    [Fact]
    public void GetReceiptsByUserIdAsync_ExistingUser_ReturnsReceipts()
    {
        // Arrange.
        var receiptDatabaseOperator = new JsonReceiptDatabaseOperator();

        var receipt1 = new Receipt(receiptDatabaseOperator.GetNewUniqueId(), 0, 10, 0, 0);
        var receipt2 = new Receipt(receiptDatabaseOperator.GetNewUniqueId(), 0, 10, 0, 0);
        var receipt3 = new Receipt(receiptDatabaseOperator.GetNewUniqueId(), 1, 10, 0, 0);

        receiptDatabaseOperator.SaveAsync(receipt1).Wait();
        receiptDatabaseOperator.SaveAsync(receipt2).Wait();
        receiptDatabaseOperator.SaveAsync(receipt3).Wait();

        // Act.
        var result = receiptDatabaseOperator.GetReceiptsByUserIdAsync(0).Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        // Cleanup.
        File.Delete(_databasePath);
    }

    [Fact]
    public void GetReceiptsByUserIdAsync_NonExistingUser_ReturnsEmptyList()
    {
        // Arrange.
        var receiptDatabaseOperator = new JsonReceiptDatabaseOperator();

        var receipt1 = new Receipt(receiptDatabaseOperator.GetNewUniqueId(), 0, 10, 0, 0);
        var receipt2 = new Receipt(receiptDatabaseOperator.GetNewUniqueId(), 0, 10, 0, 0);
        var receipt3 = new Receipt(receiptDatabaseOperator.GetNewUniqueId(), 1, 10, 0, 0);

        receiptDatabaseOperator.SaveAsync(receipt1).Wait();
        receiptDatabaseOperator.SaveAsync(receipt2).Wait();
        receiptDatabaseOperator.SaveAsync(receipt3).Wait();

        // Act.
        var result = receiptDatabaseOperator.GetReceiptsByUserIdAsync(2).Result;

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result);

        // Cleanup.
        File.Delete(_databasePath);
    }
}