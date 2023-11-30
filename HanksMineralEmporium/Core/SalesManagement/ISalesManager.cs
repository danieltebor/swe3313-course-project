using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.UserManagement;

namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Contract for a manager that handles sales.
/// </summary>
public interface ISalesManager
{
    /// <summary>
    /// Checks out the given items for the given user with the given shipping option.
    /// </summary>
    /// <param name="items">The items to checkout.</param>
    /// <param name="userId">The ID of the user to checkout the items for.</param>
    /// <param name="shippingInfo">The shipping info to use.</param>
    /// <param name="creditCardInfo">The credit card info to use.</param>
    /// <returns>The created IReceipt.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="items"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="shippingInfo"/> is null.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="creditCardInfo"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="items"/> is empty.</exception>
    /// <exception cref="ItemAlreadySoldException">Thrown when any of the given items have already been sold.</exception>
    public Task<IReceipt> CheckoutItemsAsync(IEnumerable<IItem> items, ulong userId,
                                             IShippingInfo shippingInfo, ICreditCardInfo creditCardInfo);   

    /// <summary>
    /// Gets all sales in the database.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task<IReadOnlyList<ISale>> GetAllSalesAsync();

    /// <summary>
    /// Gets the receipt associated with the given sale ID.
    /// </summary>
    /// <param name="saleId">The ID of the sale to get the receipt for.</param>
    /// <returns>The receipt associated with the given sale ID.</returns>
    /// <exception cref="SaleNotFoundException">Thrown when no sale with the given ID exists.</exception>
    public Task<IReceipt> GetReceiptForSaleAsync(ulong saleId);
}