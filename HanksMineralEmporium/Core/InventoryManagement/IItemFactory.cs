namespace HanksMineralEmporium.Core.InventoryManagement;

internal interface IItemFactory
{
    /// <summary>
    /// Creates a new <see cref="IItem"/> object.
    /// </summary>
    /// <param name="price">The price of the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="imagePath">The path to the image of the item.</param>
    /// <param name="description">The description of the item.</param>
    /// <returns>The newly created <see cref="IItem"/> object.</returns>
    public IItem CreateNewItem(decimal price, string name, string imagePath, string? description = null);
}