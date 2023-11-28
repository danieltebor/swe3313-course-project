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
    /// <exception cref="ArgumentException">Thrown when value is less than 0.</exception>
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