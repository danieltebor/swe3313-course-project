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
    /// <param name="imageFilename">The filename of the image.</param>
    /// <returns>The newly created item.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="price"/> is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="imageFilename"/> is null or empty.</exception>
    public Task<IItem> AddNewItemAsync(
        decimal price,
        string name,
        string imageFilename,
        string? description = null);

    /// <summary>
    /// Gets all available items in the inventory.
    /// </summary>
    /// <returns>A list of all available items in the inventory.</returns>
    public Task<IReadOnlyList<IItem>> GetAllAvailableItemsAsync();

    /// <summary>
    /// Gets the image of an item.
    /// </summary>
    /// <param name="imageFilename">The name of the image file.</param>
    /// <returns>The image of the item.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="imageFilename"/> is null or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the image file does not exist.</exception>
    /// <exception cref="IOException">Thrown when the image file cannot be read.</exception>
    public string GetItemImagePathAsync(string imageFilename);

    /// <summary>
    /// Gets all item images.
    /// </summary>
    /// <returns>A list of all item images.</returns>
    public IReadOnlyList<string> GetAllItemImagePathsAsync();
}