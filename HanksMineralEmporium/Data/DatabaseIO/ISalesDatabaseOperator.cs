using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for a database operator that handles <see cref="ISale"/> objects.
/// </summary>
internal interface ISalesDatabaseOperator : IDatabaseOperator<ISale>
{
    /// <summary>
    /// Compiles a list of all sales associated with the given receipt ID.
    /// </summary>
    /// <param name="receiptId">The ID of the receipt to get sales for.</param>
    /// <returns>List of all sales associated with the given receipt ID.</returns>
    public Task<IReadOnlyList<ISale>> GetSalesByReceiptIdAsync(ulong receiptId);

    /// <summary>
    /// Gets a list of all item IDs that have been sold.
    /// </summary>
    /// <returns>List of all item IDs that have been sold.</returns>
    public Task<IReadOnlyList<ulong>> GetSoldItemIdsAsync();
}