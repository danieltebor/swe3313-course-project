namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Contract for a factory that creates <see cref="ISale"/> objects.
/// </summary>
internal interface ISaleFactory
{
    /// <summary>
    /// Creates a new sale with the given user ID, item ID, and receipt ID. The sale is saved in the database.
    /// </summary>
    /// <param name="itemId">The ID of the item to create the sale for.</param>
    /// <param name="receiptId">The ID of the receipt to create the sale for.</param>
    /// <returns>The created ISale.</returns>
    public ISale CreateNewSale(ulong itemId, ulong receiptId);

    /// <summary>
    /// Gets an existing sale by its ID.
    /// </summary>
    /// <param name="id">The ID of the sale to load.</param>
    /// <returns>The retreived ISale</returns>
    /// <exception cref="SaleNotFoundException">Thrown when no sale with the given ID exists.</exception>
    public ISale GetSaleById(ulong id);

    /// <summary>
    /// Compiles a list of all sales associated with the given receipt ID.
    /// </summary>
    /// <param name="receiptId">The ID of the receipt to get sales for.</param>
    /// <returns>List of all sales associated with the given receipt ID.</returns>
    public IReadOnlyList<ISale> GetSalesByReceiptId(ulong receiptId);
}