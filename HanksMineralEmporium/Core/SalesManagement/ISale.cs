using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Contract for a sale.
/// </summary>
public interface ISale : IDatabaseObject
{
    /// <summary>
    /// The ID of the receipt this sale is associated with.
    /// </summary>
    public ulong ReceiptId { get; }

    /// <summary>
    /// The ID of the item this sale is associated with.
    /// </summary>
    public ulong ItemId { get; }
}