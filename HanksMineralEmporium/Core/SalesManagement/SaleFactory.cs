using HanksMineralEmporium.Core.SalesManagement.Exception;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.SalesManagement;

internal class SaleFactory : ISaleFactory
{
    private readonly ISalesDatabaseOperator _salesDatabaseOperator;

    public SaleFactory(ISalesDatabaseOperator salesDatabaseOperator) {
        _salesDatabaseOperator = salesDatabaseOperator 
            ?? throw new ArgumentNullException(nameof(salesDatabaseOperator));
    }

    /// <inheritdoc/>
    public ISale CreateNewSale(ulong itemId, ulong receiptId)
    {
        var sale = new Sale(_salesDatabaseOperator.GetNewUniqueId(), itemId, receiptId);
        _salesDatabaseOperator.SaveAsync(sale).Wait();

        return sale;
    }

    /// <inheritdoc/>
    public ISale GetSaleById(ulong id)
    {
        var sale = _salesDatabaseOperator.GetByIdAsync(id).Result
            ?? throw new SaleNotFoundException(id);

        return sale;
    }

    /// <inheritdoc/>
    public IReadOnlyList<ISale> GetSalesByReceiptId(ulong receiptId)
    {
        return _salesDatabaseOperator.GetSalesByReceiptIdAsync(receiptId).Result;
    }
}