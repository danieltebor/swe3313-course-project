using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement.Exception;
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Implementation of <see cref="ISalesManager"/>.
/// </summary>
internal class SalesManager : ISalesManager
{
    private readonly ISalesDatabaseOperator _salesDatabaseOperator;
    private readonly IReceiptDatabaseOperator _receiptDatabaseOperator;
    private readonly ISaleFactory _saleFactory;
    private readonly IReceiptFactory _receiptFactory;

    private readonly ISet<ulong> _transientItemIds = new HashSet<ulong>();
    private readonly SemaphoreSlim _checkoutLock = new SemaphoreSlim(1, 1);

    public SalesManager( ISalesDatabaseOperator salesDatabaseOperator, IReceiptDatabaseOperator receiptDatabaseOperator)
    {
        if (salesDatabaseOperator is null)
        {
            throw new ArgumentNullException(nameof(salesDatabaseOperator));
        }
        else if (receiptDatabaseOperator is null)
        {
            throw new ArgumentNullException(nameof(receiptDatabaseOperator));
        }

        _salesDatabaseOperator = salesDatabaseOperator;
        _receiptDatabaseOperator = receiptDatabaseOperator;
        _saleFactory = new SaleFactory(salesDatabaseOperator);
        _receiptFactory = new ReceiptFactory(receiptDatabaseOperator);
    }

    /// <inheritdoc/>
    public async Task<IReceipt> CheckoutItemsAsync(IEnumerable<IItem> items, ulong userId, ShippingOption shippingOption)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }
        else if (!Enum.IsDefined(typeof(ShippingOption), shippingOption))
        {
            throw new ArgumentException("Invalid shipping option.", nameof(shippingOption));
        }
        if (items.Any(item => item == null))
        {
            throw new ArgumentException("One of the items is null.", nameof(items));
        }

        await _checkoutLock.WaitAsync();
        try
        {
            var previousSoldItemIds = await _salesDatabaseOperator.GetSoldItemIdsAsync();
            var alreadySoldItemIds = items
                .Where(item => previousSoldItemIds.Contains(item.Id))
                .Select(item => item.Id);
            if (alreadySoldItemIds.Count() > 0)
            {
                throw new ItemsAlreadySoldException(alreadySoldItemIds);
            }

            var subtotal = items.Sum(item => item.Price);
            var shipping = (decimal)shippingOption;
            var tax = subtotal * 0.06m;
            var receipt = _receiptFactory.CreateNewReceipt(userId, subtotal, shipping, tax);

            foreach (var item in items)
            {   
                _saleFactory.CreateNewSale(item.Id, receipt.Id);
            };

            return receipt;
        }
        finally
        {
            _checkoutLock.Release();
        }
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ISale>> GetAllSalesAsync()
    {
        return await _salesDatabaseOperator.GetAllAsync();
    }

    /// <inheritdoc/>
    public async Task<IReceipt> GetReceiptForSaleAsync(ulong saleId)
    {
        var sale = await _salesDatabaseOperator.GetByIdAsync(saleId)
            ?? throw new SaleNotFoundException(saleId);

        return await _receiptDatabaseOperator.GetByIdAsync(sale.ReceiptId)
            ?? throw new ReceiptNotFoundException(sale.ReceiptId);
    }
}