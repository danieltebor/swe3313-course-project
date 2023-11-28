using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Data.DatabaseIO;

/// <summary>
/// Contract for a database operator that handles <see cref="IReceipt"/> objects.
/// </summary>
internal interface IReceiptDatabaseOperator : IDatabaseOperator<IReceipt>
{
    /// <summary>
    /// Compiles a list of all receipts associated with the given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to get receipts for.</param>
    /// <returns>List of all receipts associated with the given user ID.</returns>
    public Task<IReadOnlyList<IReceipt>> GetReceiptsByUserIdAsync(ulong userId);
}