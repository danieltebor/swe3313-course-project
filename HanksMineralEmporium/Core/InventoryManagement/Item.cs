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
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="imagePath"/> is null or whitespace,
    /// or when price is less than 0.</exception>
    public Item(ulong id, decimal price, string name, string description, string imagePath)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
        else if (string.IsNullOrWhiteSpace(imagePath))
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
    public decimal Price { 
        get => Price; 
        set => Price = value < 0 ? throw new ArgumentException("Value cannot be less than 0.", nameof(value)) : value;
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string ImagePath { get; set; }
}