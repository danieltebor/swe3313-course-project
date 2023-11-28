using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.InventoryManagement;

internal class ItemFactory : IItemFactory
{
    private readonly IItemDatabaseOperator _itemDatabaseOperator;

    public ItemFactory(IItemDatabaseOperator itemDatabaseOperator)
    {
        _itemDatabaseOperator = itemDatabaseOperator ?? throw new ArgumentNullException(nameof(itemDatabaseOperator));
    }

    /// <inheritdoc/>
    public IItem CreateNewItem(decimal price, string name, string imagePath, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
        else if (string.IsNullOrWhiteSpace(imagePath))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(imagePath));
        }

        var item = new Item(_itemDatabaseOperator.GetNewUniqueId(), price, name, description, imagePath);
        _itemDatabaseOperator.SaveAsync(item).Wait();
        return item;
    }
}
