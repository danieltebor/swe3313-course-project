using System.Diagnostics.CodeAnalysis;

using HanksMineralEmporium.Core.SalesManagement.Exception;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.SalesManagement;

internal class ReceiptFactory : IReceiptFactory
{
    [NotNull]
    private readonly IReceiptDatabaseOperator _receiptDatabaseOperator;

    public ReceiptFactory([DisallowNull] IReceiptDatabaseOperator receiptDatabaseOperator) {
        _receiptDatabaseOperator = receiptDatabaseOperator 
            ?? throw new ArgumentNullException(nameof(receiptDatabaseOperator));
    }

    /// <inheritdoc/>
    public IReceipt CreateNewReceipt(ulong userId, decimal subtotal, decimal shipping, decimal tax)
    {
        var receipt = new Receipt(_receiptDatabaseOperator.GetNewUniqueId(), userId, subtotal, shipping, tax);
        _receiptDatabaseOperator.SaveAsync(receipt).Wait();

        return receipt;
    }

    /// <inheritdoc/>
    public IReceipt GetReceiptById(ulong id)
    {
        var receipt = _receiptDatabaseOperator.GetByIdAsync(id).Result
            ?? throw new ReceiptNotFoundException(id);

        return receipt;
    }

    /// <inheritdoc/>
    public IReadOnlyList<IReceipt> GetReceiptsByUserId(ulong userId)
    {
        return _receiptDatabaseOperator.GetReceiptsByUserIdAsync(userId).Result;
    }
}