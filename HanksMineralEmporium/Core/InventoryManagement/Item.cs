using System.Diagnostics.CodeAnalysis;

namespace HanksMineralEmporium.Core.InventoryManagement;

/// <summary>
/// Represents an item in the inventory.
/// </summary>
public class Item : IItem
{
    /// <summary>
    /// Creates a new Item object.
    /// </summary>
    /// <param name="id">The id unique id of the item.</param>
    /// <param name="price">The price of the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">The description of the item.</param>
    /// <param name="imagePath">The path to the image of the item.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="imagePath"/> is null or whitespace.</exception>
    public Item([DisallowNull] ulong id, decimal price, [DisallowNull] string name, [AllowNull] string description, [DisallowNull] string imagePath)
    {
        if (name.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
        else if (imagePath.IsNullOrWhiteSpace())
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(imagePath));
        }
        else if (price < 0)
        {
            throw new ArgumentException("Value cannot be less than 0.", nameof(price));
        }

        Id = id;
        Price = price;
        Name = name;
        Description = description;
        ImagePath = imagePath;
    }

    public ulong Id { get; }
    public decimal Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}