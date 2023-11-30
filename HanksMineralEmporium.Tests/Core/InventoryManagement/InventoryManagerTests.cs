using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.InventoryManagement.Tests;

public class InventoryManagerTests
{
    [Fact]
    public void AddNewItemAsync_WithValidArguments_CreatesNewItem()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        mockItemDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<IItem>())).Returns(Task.CompletedTask);
        mockItemDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(10);

        var mockSalesManager = new Mock<ISalesManager>();

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var item = inventoryManager.AddNewItemAsync(1, "Test", "Test").Result;

        // Assert
        Assert.NotNull(item);
        Assert.Equal(10ul, item.Id);
    }

    [Fact]
    public void AddNewItemAsync_WithNullandEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        var mockSalesManager = new Mock<ISalesManager>();

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var exception1 = Assert.ThrowsAsync<ArgumentException>(() => inventoryManager.AddNewItemAsync(1, null!, "Test"));
        var exception2 = Assert.ThrowsAsync<ArgumentException>(() => inventoryManager.AddNewItemAsync(1, "", "Test"));

        // Assert
        Assert.NotNull(exception1);
        Assert.NotNull(exception2);
    }

    [Fact]
    public void AddNewItemAsync_WithNullandEmptyImagePath_ThrowsArgumentException()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        var mockSalesManager = new Mock<ISalesManager>();

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var exception1 = Assert.ThrowsAsync<ArgumentException>(() => inventoryManager.AddNewItemAsync(1, "Test", null!));
        var exception2 = Assert.ThrowsAsync<ArgumentException>(() => inventoryManager.AddNewItemAsync(1, "Test", ""));

        // Assert
        Assert.NotNull(exception1);
        Assert.NotNull(exception2);
    }

    [Fact]
    public void GetAllAvailableItemsAsync_WithNoItems_ReturnsEmptyList()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        mockItemDatabaseOperator.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<IItem>());
                

        var mockSalesManager = new Mock<ISalesManager>();
        mockSalesManager.Setup(x => x.GetAllSalesAsync()).ReturnsAsync(
            new List<ISale>()
            {
                new Sale(0, 0, 0),
                new Sale(1, 1, 0),
                new Sale(2, 2, 0)
            }
        );

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var items = inventoryManager.GetAllAvailableItemsAsync().Result;

        // Assert
        Assert.NotNull(items);
        Assert.Empty(items);
    }

    [Fact]
    public void GetAllAvailableItemsAsync_WithItems_ReturnsItems()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        mockItemDatabaseOperator.Setup(x => x.GetAllAsync()).ReturnsAsync(
            new List<IItem>()
            {
                new Item(0, 10, "Test", "Test", "Test"),
                new Item(1, 10, "Test", "Test", "Test"),
                new Item(2, 10, "Test", "Test", "Test"),
                new Item(3, 10, "Test", "Test", "Test"),
                new Item(4, 10, "Test", "Test", "Test"),
            }
        );

        var mockSalesManager = new Mock<ISalesManager>();
        mockSalesManager.Setup(x => x.GetAllSalesAsync()).ReturnsAsync(
            new List<ISale>()
            {
                new Sale(0, 0, 0),
                new Sale(1, 1, 0),
                new Sale(2, 2, 1)
            }
        );

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var items = inventoryManager.GetAllAvailableItemsAsync().Result;

        // Assert
        Assert.NotNull(items);
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void GetAllAvailableItemsAsync_WithNoSales_ReturnsAllItems()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        mockItemDatabaseOperator.Setup(x => x.GetAllAsync()).ReturnsAsync(
            new List<IItem>()
            {
                new Item(0, 10, "Test", "Test", "Test"),
                new Item(1, 10, "Test", "Test", "Test"),
                new Item(2, 10, "Test", "Test", "Test"),
                new Item(3, 10, "Test", "Test", "Test"),
                new Item(4, 10, "Test", "Test", "Test"),
            }
        );

        var mockSalesManager = new Mock<ISalesManager>();
        mockSalesManager.Setup(x => x.GetAllSalesAsync()).ReturnsAsync(new List<ISale>());

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var items = inventoryManager.GetAllAvailableItemsAsync().Result;

        // Assert
        Assert.NotNull(items);
        Assert.Equal(5, items.Count);
    }

    [Fact]
    public void GetAllAvailableItemsAsync_WithAllSales_ReturnsEmptyList()
    {
        // Arrange
        var mockItemDatabaseOperator = new Mock<IItemDatabaseOperator>();
        mockItemDatabaseOperator.Setup(x => x.GetAllAsync()).ReturnsAsync(
            new List<IItem>()
            {
                new Item(0, 10, "Test", "Test", "Test"),
                new Item(1, 10, "Test", "Test", "Test"),
                new Item(2, 10, "Test", "Test", "Test"),
                new Item(3, 10, "Test", "Test", "Test"),
                new Item(4, 10, "Test", "Test", "Test"),
            }
        );

        var mockSalesManager = new Mock<ISalesManager>();
        mockSalesManager.Setup(x => x.GetAllSalesAsync()).ReturnsAsync(
            new List<ISale>()
            {
                new Sale(0, 0, 0),
                new Sale(1, 1, 1),
                new Sale(2, 2, 2),
                new Sale(3, 3, 3),
                new Sale(4, 4, 0),
            }
        );

        var inventoryManager = new InventoryManager(mockItemDatabaseOperator.Object, mockSalesManager.Object);

        // Act
        var items = inventoryManager.GetAllAvailableItemsAsync().Result;

        // Assert
        Assert.NotNull(items);
        Assert.Empty(items);
    }
}