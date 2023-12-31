using HanksMineralEmporium.Core.InventoryManagement;

namespace HanksMineralEmporium.Service.InventoryService;

/// <summary>
/// Service that provides <see cref="IItem"/> objects.
/// </summary>
internal class InventoryService : IInventoryService
{
    private readonly IInventoryManager _inventoryManager;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="inventoryManager"></param>
    public InventoryService(IInventoryManager inventoryManager)
    {
        _inventoryManager = inventoryManager ?? throw new ArgumentNullException(nameof(inventoryManager));
    }

    /// <inheritdoc/>
    public async Task<IItem> AddNewItemAsync(
        decimal price,
        string name,
        string imageFilename,
        string? description = null)
    {
        return await _inventoryManager.AddNewItemAsync(price, name, imageFilename, description);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<IItem>> GetAllAvailableItemsAsync()
    {
        return await _inventoryManager.GetAllAvailableItemsAsync();
    }

    /// <inheritdoc/>
    public string GetItemImagePath(string imageFilename)
    {
        return _inventoryManager.GetItemImagePath(imageFilename);
    }
}