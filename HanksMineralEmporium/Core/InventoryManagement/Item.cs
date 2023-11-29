namespace HanksMineralEmporium.Core.InventoryManagement;

/// <summary>
/// Represents an item in the inventory.
/// </summary>
internal class Item : IItem
{
    private decimal _price;

    /// <summary>
    /// Creates a new Item object.
    /// </summary>
    /// <param name="id">The id unique id of the item.</param>
    /// <param name="price">The price of the item.</param>
    /// <param name="name">The name of the item.</param>
    /// <param name="description">The description of the item.</param>
    /// <param name="imageFilename">The name of the image file.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="name"/> or <paramref name="imageFilename"/> is null or whitespace,
    /// or when price is less than 0.</exception>
    public Item(ulong id, decimal price, string name, string? description, string imageFilename)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
        }
        else if (string.IsNullOrWhiteSpace(imageFilename))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(imageFilename));
        }

        Id = id;
        Price = price;
        Name = name;
        Description = description;
        ImageFilename = imageFilename;
    }

    public ulong Id { get; }
    public decimal Price { 
        get => _price; 
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));
            }

            _price = value;
        }
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string ImageFilename { get; set; }
}