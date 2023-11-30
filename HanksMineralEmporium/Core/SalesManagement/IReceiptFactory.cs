namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Contract for a factory that creates <see cref="IReceipt"/> objects.
/// </summary>
internal interface IReceiptFactory
{
    /// <summary>
    /// Creates a new receipt with the given subtotal, shipping, and tax. The id is generated automatically
    /// and the receipt is saved in the database.
    /// </summary>
    /// <param name="userId">The ID of the user to create the receipt for.</param>
    /// <param name="subtotal">The subtotal of the order.</param>
    /// <param name="shipping">The shipping cost of the order.</param>
    /// <param name="tax">The tax of the order.</param>
    /// <returns>The created IReceipt.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="subtotal"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="shipping"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="tax"/> is less than 0.</exception>
    public IReceipt CreateNewReceipt(ulong userId, decimal subtotal, decimal shipping, decimal tax);

    /// <summary>
    /// Gets an existing receipt by its ID.
    /// </summary>
    /// <param name="id">The ID of the receipt to load.</param>
    /// <returns>The retreived IReceipt</returns>
    /// <exception cref="ReceiptNotFoundException">Thrown when no receipt with the given ID exists.</exception>
    public IReceipt GetReceiptById(ulong id);

    /// <summary>
    /// Compiles a list of all receipts associated with the given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to get receipts for.</param>
    /// <returns>List of all receipts associated with the given user ID.</returns>
    public IReadOnlyList<IReceipt> GetReceiptsByUserId(ulong userId);
}