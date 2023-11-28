namespace HanksMineralEmporium.Core.SalesManagement;

internal class Receipt : IReceipt
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Receipt"/> class.
    /// </summary>
    /// <param name="id">The receipt's unique identifier.</param>
    /// <param name="userId">The ID of the customer this receipt is associated with.</param>
    /// <param name="subtotal">The subtotal of the order.</param>
    /// <param name="shipping">The shipping cost of the order.</param>
    /// <param name="tax">The tax of the order.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="subtotal"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="shipping"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="tax"/> is less than 0.</exception>
    public Receipt(ulong id, ulong userId, decimal subtotal, decimal shipping, decimal tax)
    {
        if (subtotal < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(subtotal), subtotal, "Subtotal cannot be less than 0.");
        }
        else if (shipping < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(shipping), shipping, "Shipping cannot be less than 0.");
        }
        else if (tax < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tax), tax, "Tax cannot be less than 0.");
        }

        Id = id;
        UserId = userId;
        Subtotal = subtotal;
        Shipping = shipping;
        Tax = tax;
    }

    public ulong Id { get; }
    public ulong UserId { get; }
    public decimal Subtotal { get; }
    public decimal Shipping { get; }
    public decimal Tax { get; }
    public decimal Total
    {
        get
        {
            return Subtotal + Shipping + Tax;
        }
    }
}