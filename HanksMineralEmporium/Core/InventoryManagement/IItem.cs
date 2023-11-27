using System.Diagnostics.CodeAnalysis;

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
    [NotNull]
    public decimal Price { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    [NotNull]
    public string Name { get; set; }

    /// <summary>
    /// The description of the item.
    /// </summary>
    [Nullable]
    public string Description { get; set; }

    /// <summary>
    /// The path to the image of the item.
    /// </summary>
    [NotNull]
    public string ImagePath { get; set; }
}