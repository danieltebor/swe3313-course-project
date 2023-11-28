using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Core.SalesManagement.Exception;
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.SalesManagement.Tests;

public class SalesManagerTests
{
    [Fact]
    public void CheckoutItemsAsync_WithValidArguments_CreatesReceiptandSales()
    {
        // Arrange
        var items = new List<IItem>()
        {
            new Item(0, 10m, "Test Item 1", "Test Description 1", "Test Image Path 1"),
            new Item(1, 20m, "Test Item 2", "Test Description 2", "Test Image Path 2"),
            new Item(2, 30m, "Test Item 3", "Test Description 3", "Test Image Path 3"),
        };
        var shippingOption = ShippingOption.Ground;

        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<ISale>()))
            .Returns(Task.CompletedTask);
        var saleId = 0ul;
        mockSalesDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => saleId++);
        mockSalesDatabaseOperator.Setup(x => x.GetSoldItemIdsAsync())
            .ReturnsAsync(
                new List<ulong>()
                {
                    3
                }
            );

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<IReceipt>()))
            .Returns(Task.CompletedTask);
        var receiptId = 0ul;
        mockReceiptDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => receiptId++);

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var receipt = salesManager.CheckoutItemsAsync(items, 0, shippingOption).Result;

        // Assert
        Assert.NotNull(receipt);
        Assert.Equal(60m, receipt.Subtotal);
        Assert.Equal(3.6m, receipt.Tax);
    }

    [Fact]
    public void CheckoutItemsAsync_WithItemsAlreadySold_ThrowsItemsAlreadySoldException()
    {
        // Arrange
        var items = new List<IItem>()
        {
            new Item(0, 10m, "Test Item 1", "Test Description 1", "Test Image Path 1"),
            new Item(1, 20m, "Test Item 2", "Test Description 2", "Test Image Path 2"),
            new Item(2, 30m, "Test Item 3", "Test Description 3", "Test Image Path 3"),
        };
        var shippingOption = ShippingOption.Ground;

        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<ISale>()))
            .Returns(Task.CompletedTask);
        var saleId = 0ul;
        mockSalesDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => saleId++);
        mockSalesDatabaseOperator.Setup(x => x.GetSoldItemIdsAsync())
            .ReturnsAsync(
                new List<ulong>()
                {
                    0
                }
            );

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<IReceipt>()))
            .Returns(Task.CompletedTask);
        var receiptId = 0ul;
        mockReceiptDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => receiptId++);

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var exception = Assert.ThrowsAsync<ItemsAlreadySoldException>(() => salesManager.CheckoutItemsAsync(items, 0, shippingOption));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void CheckoutItemsAsync_WithEmptyItems_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var items = new List<IItem>();
        var shippingOption = ShippingOption.Ground;

        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<ISale>()))
            .Returns(Task.CompletedTask);
        var saleId = 0ul;
        mockSalesDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => saleId++);
        mockSalesDatabaseOperator.Setup(x => x.GetSoldItemIdsAsync())
            .ReturnsAsync(new List<ulong>());

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<IReceipt>()))
            .Returns(Task.CompletedTask);
        var receiptId = 0ul;
        mockReceiptDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => receiptId++);

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var exception = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => salesManager.CheckoutItemsAsync(items, 0, shippingOption));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void CheckoutItemsAsync_WithNullItems_ThrowsArgumentNullException()
    {
        // Arrange
        var listWithNullItems = new List<IItem>()
        {
            new Item(0, 10m, "Test Item 1", "Test Description 1", "Test Image Path 1"),
            null!,
            new Item(2, 30m, "Test Item 3", "Test Description 3", "Test Image Path 3"),
        };
        var shippingOption = ShippingOption.Ground;

        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<ISale>()))
            .Returns(Task.CompletedTask);
        var saleId = 0ul;
        mockSalesDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => saleId++);
        mockSalesDatabaseOperator.Setup(x => x.GetSoldItemIdsAsync())
            .ReturnsAsync(new List<ulong>());

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<IReceipt>()))
            .Returns(Task.CompletedTask);
        var receiptId = 0ul;
        mockReceiptDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => receiptId++);

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var exception1 = Assert.ThrowsAsync<ArgumentNullException>(() => salesManager.CheckoutItemsAsync(null!, 0, shippingOption));
        var exception2 = Assert.ThrowsAsync<ArgumentNullException>(() => salesManager.CheckoutItemsAsync(listWithNullItems, 0, shippingOption));

        // Assert
        Assert.NotNull(exception1);
        Assert.NotNull(exception2);
    }

    [Fact]
    public void CheckoutItemsAsync_WithInvalidShippingOption_ThrowsArgumentException()
    {
        // Arrange
        var items = new List<IItem>()
        {
            new Item(0, 10m, "Test Item 1", "Test Description 1", "Test Image Path 1"),
            new Item(1, 20m, "Test Item 2", "Test Description 2", "Test Image Path 2"),
            new Item(2, 30m, "Test Item 3", "Test Description 3", "Test Image Path 3"),
        };
        var shippingOption = (ShippingOption)int.MaxValue;

        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<ISale>()))
            .Returns(Task.CompletedTask);
        var saleId = 0ul;
        mockSalesDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => saleId++);
        mockSalesDatabaseOperator.Setup(x => x.GetSoldItemIdsAsync())
            .ReturnsAsync(new List<ulong>());

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.SaveAsync(It.IsAny<IReceipt>()))
            .Returns(Task.CompletedTask);
        var receiptId = 0ul;
        mockReceiptDatabaseOperator.Setup(x => x.GetNewUniqueId()).Returns(() => receiptId++);

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var exception = Assert.ThrowsAsync<ArgumentException>(() => salesManager.CheckoutItemsAsync(items, 0, shippingOption));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void GetAllSalesAsync_WithExistingSales_ReturnsSales()
    {
        // Arrange
        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.GetAllAsync())
            .ReturnsAsync(
                new List<ISale>()
                {
                    new Sale(0, 0, 0),
                    new Sale(1, 1, 0),
                    new Sale(2, 2, 0),
                }
            );

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var result = salesManager.GetAllSalesAsync().Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetAllSalesAsync_WithNoSales_ReturnsEmptyList()
    {
        // Arrange
        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<ISale>());

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var result = salesManager.GetAllSalesAsync().Result;

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetReceiptForSaleAsync_WithExistingSale_ReturnsReceipt()
    {
        // Arrange
        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.GetByIdAsync(0))
            .ReturnsAsync(new Sale(0, 0, 0));

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.GetByIdAsync(0))
            .ReturnsAsync(new Receipt(0, 0, 0, 0, 0));

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var result = salesManager.GetReceiptForSaleAsync(0).Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0ul, result.Id);
    }

    [Fact]
    public void GetReceiptForSaleAsync_WithNonExistingSale_ThrowsSaleNotFoundException()
    {
        // Arrange
        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.GetByIdAsync(0))
            .ReturnsAsync((ISale)null!);

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var exception = Assert.ThrowsAsync<SaleNotFoundException>(() => salesManager.GetReceiptForSaleAsync(0));

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void GetReceiptForSaleAsync_WithExistingSaleButNonExistingReceipt_ThrowsReceiptNotFoundException()
    {
        // Arrange
        var mockSalesDatabaseOperator = new Mock<ISalesDatabaseOperator>();
        mockSalesDatabaseOperator.Setup(x => x.GetByIdAsync(0))
            .ReturnsAsync(new Sale(0, 0, 0));

        var mockReceiptDatabaseOperator = new Mock<IReceiptDatabaseOperator>();
        mockReceiptDatabaseOperator.Setup(x => x.GetByIdAsync(0))
            .ReturnsAsync((IReceipt)null!);

        var salesManager = new SalesManager(mockSalesDatabaseOperator.Object, mockReceiptDatabaseOperator.Object);

        // Act
        var exception = Assert.ThrowsAsync<ReceiptNotFoundException>(() => salesManager.GetReceiptForSaleAsync(0));

        // Assert
        Assert.NotNull(exception);
    }
}