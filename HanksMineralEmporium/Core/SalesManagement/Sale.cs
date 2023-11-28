namespace HanksMineralEmporium.Core.SalesManagement;

internal class Sale : ISale
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Sale"/> class.
    /// </summary>
    /// <param name="id">The sale's unique identifier.</param>
    /// <param name="itemId">The ID of the item that was sold.</param>
    /// /// <param name="receiptId">The ID of the receipt that the sale is associated with.</param>
    public Sale(ulong id, ulong itemId, ulong receiptId)
    {
        Id = id;
        ItemId = itemId;
        ReceiptId = receiptId;
    }

    public ulong Id { get; }
    public ulong ItemId { get; }
    public ulong ReceiptId { get; }
}