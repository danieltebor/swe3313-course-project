using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.InventoryManagement;

internal class InventoryManager : IInventoryManager
{
    private readonly IItemDatabaseOperator _itemDatabaseOperator;
    private readonly ISalesManager _salesManager;
    private readonly IItemFactory _itemFactory;

    public InventoryManager(IItemDatabaseOperator itemDatabaseOperator, ISalesManager salesManager)
    {
        _itemDatabaseOperator = itemDatabaseOperator ?? throw new ArgumentNullException(nameof(itemDatabaseOperator));
        _salesManager = salesManager ?? throw new ArgumentNullException(nameof(salesManager));

        _itemFactory = new ItemFactory(itemDatabaseOperator);
    }

    /// <inheritdoc/>
    public async Task<IItem> AddNewItemAsync(decimal price, string name, string imagePath, string? description = null)
    {
        return await Task.Run(() => _itemFactory.CreateNewItem(price, name, imagePath, description));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<IItem>> GetAllAvailableItemsAsync()
    {
        var soldItemIds = (await _salesManager.GetAllSalesAsync())
            .Select(sale => sale.ItemId);

        return (await _itemDatabaseOperator.GetAllAsync())
            .Where(item => !soldItemIds.Contains(item.Id)).ToList();
    }
}