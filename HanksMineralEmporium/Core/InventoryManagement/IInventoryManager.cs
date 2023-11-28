namespace HanksMineralEmporium.Core.InventoryManagement;

/// <summary>
/// Contract for a manager that handles <see cref="IItem"/> objects.
/// </summary>
public interface IInventoryManager
{
    /// <summary>
    /// Adds a new item to the inventory.
    /// </summary>
    /// <param name="price">The price of the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">The description of the item.</param>
    /// <param name="imagePath">The path to the image of the item.</param>
    /// <returns>The newly created item.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="price"/> is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="imagePath"/> is null or empty.</exception>
    public Task<IItem> AddNewItemAsync(
        decimal price,
        string name,
        string imagePath,
        string? description = null);

    /// <summary>
    /// Gets all available items in the inventory.
    /// </summary>
    /// <returns>A list of all available items in the inventory.</returns>
    public Task<IReadOnlyList<IItem>> GetAllAvailableItemsAsync();
}