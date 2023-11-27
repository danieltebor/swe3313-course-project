using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.InventoryManagement;

/// <summary>
/// Contract for an item in the inventory.
/// </summary>
public interface IItem : IDatabaseObject
{
    /// <summary>
    /// The price of the item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the item.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The path to the image of the item.
    /// </summary>
    public string ImagePath { get; set; }
}