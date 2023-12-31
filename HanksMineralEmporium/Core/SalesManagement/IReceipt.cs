using HanksMineralEmporium.Data.DatabaseIO;

namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Contract for a receipt.
/// </summary>
public interface IReceipt : IDatabaseObject
{
    /// <summary>
    /// The ID of the customer this receipt is associated with.
    /// </summary>
    public ulong UserId { get; }

    /// <summary>
    /// The subtotal of the order.
    /// </summary>
    public decimal Subtotal { get; }

    /// <summary>
    /// The shipping cost of the order.
    /// </summary>
    public decimal Shipping { get; }

    /// <summary>
    /// The tax of the order.
    /// </summary>
    public decimal Tax { get; }

    /// <summary>
    /// The total cost of the order.
    /// </summary>
    public decimal Total { get; }

    /// <summary>
    /// The street address of the order.
    /// </summary>
    public string StreetAddress { get; }

    /// <summary>
    /// The city of the order.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// The state of the order.
    /// </summary>
    public string State { get; }

    /// <summary>
    /// The zip code of the order.
    /// </summary>
    public string ZipCode { get; }

    /// <summary>
    /// Last four digits of the credit card used to pay for the order.
    /// </summary>
    public string LastFourCreditCardDigits { get; }
}