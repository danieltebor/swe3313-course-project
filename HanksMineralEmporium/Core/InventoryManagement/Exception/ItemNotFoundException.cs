using HanksMineralEmporium.Data.DatabaseIO.Exception;

namespace HanksMineralEmporium.Core.InventoryManagement.Exception;

/// <summary>
/// Exception thrown when an item is not found.
/// </summary>
public class ItemNotFoundException : System.Exception
{
    /// <summary>
    /// Creates a new <see cref="ItemNotFoundException"/>.
    /// </summary>
    /// <param name="id">The ID of the item that was not found.</param>
    public ItemNotFoundException(ulong id) : base($"Item with ID {id} not found.") {}

    /// <summary>
    /// Creates a new <see cref="ItemNotFoundException"/>.
    /// </summary>
    public ItemNotFoundException(DatabaseObjectNotFoundException<IItem> innerException) {
        if (innerException is null)
        {
            throw new ArgumentNullException(nameof(innerException));
        }

        throw new ItemNotFoundException(innerException.Id);
    }
}