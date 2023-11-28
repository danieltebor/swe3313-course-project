namespace HanksMineralEmporium.Core.SalesManagement.Exception;

/// <summary>
/// Exception thrown when one or more items have already been sold.
/// </summary>
public class ItemsAlreadySoldException : System.Exception
{
    public IEnumerable<ulong> SoldItemIds { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsAlreadySoldException"/> class.
    /// </summary>
    /// <param name="soldItemIds">The IDs of the items that have already been sold.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="soldItemIds"/> is null.</exception>
    public ItemsAlreadySoldException(IEnumerable<ulong> soldItemIds)
        : base("Some items have already been sold.")
    {
        if (soldItemIds is null)
        {
            throw new ArgumentNullException(nameof(soldItemIds));
        }

        SoldItemIds = soldItemIds;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsAlreadySoldException"/> class.
    /// </summary>
    /// <param name="soldItemIds">The IDs of the items that have already been sold.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="soldItemIds"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is empty.</exception>
    public ItemsAlreadySoldException(IEnumerable<ulong> soldItemIds, string message)
        : base(message)
    {
        if (soldItemIds is null)
        {
            throw new ArgumentNullException(nameof(soldItemIds));
        }
        else if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Message cannot be null or empty.", nameof(message));
        }

        SoldItemIds = soldItemIds;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsAlreadySoldException"/> class.
    /// </summary>
    /// <param name="soldItemIds">The IDs of the items that have already been sold.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="soldItemIds"/> is null.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="inner"/> is null.</exception>
    public ItemsAlreadySoldException(IEnumerable<ulong> soldItemIds, string message, System.Exception inner)
        : base(message, inner)
    {
        if (soldItemIds is null)
        {
            throw new ArgumentNullException(nameof(soldItemIds));
        }
        else if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Message cannot be null or empty.", nameof(message));
        }
        else if (inner is null)
        {
            throw new ArgumentNullException(nameof(inner));
        }

        SoldItemIds = soldItemIds;
    }
}