using System.Text.RegularExpressions;

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
    /// <param name="streetAddress">The street address of the order.</param>
    /// <param name="city">The city of the order.</param>
    /// <param name="state">The state of the order.</param>
    /// <param name="zipCode">The zip code of the order.</param>
    /// <param name="lastFourCreditCardDigits">Last four digits of the credit card used to pay for the order.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="subtotal"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="shipping"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="tax"/> is less than 0.</exception>
    /// <exception cref="ArgumentException">Thrown when any of the string parameters are null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="lastFourCreditCardDigits"/> is not 4 digits.</exception>
    public Receipt(ulong id, ulong userId, decimal subtotal, decimal shipping, decimal tax,
                   string streetAddress, string city, string state, string zipCode, string lastFourCreditCardDigits)
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
        else if (string.IsNullOrWhiteSpace(streetAddress))
        {
            throw new ArgumentException("Street Address cannot be null or empty.", nameof(streetAddress));
        }
        else if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City cannot be null or empty.", nameof(city));
        }
        else if (string.IsNullOrWhiteSpace(state))
        {
            throw new ArgumentException("State cannot be null or empty.", nameof(state));
        }
        else if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new ArgumentException("Zip Code cannot be null or empty.", nameof(zipCode));
        }
        else if (string.IsNullOrWhiteSpace(lastFourCreditCardDigits))
        {
            throw new ArgumentException("Last Four Credit Card Digits cannot be null or empty.", nameof(lastFourCreditCardDigits));
        }
        else if (!Regex.IsMatch(lastFourCreditCardDigits, @"^\d{4}$"))
        {
            throw new ArgumentException("Last Four Credit Card Digits must be 4 digits.", nameof(lastFourCreditCardDigits));
        }

        Id = id;
        UserId = userId;
        Subtotal = subtotal;
        Shipping = shipping;
        Tax = tax;
        StreetAddress = streetAddress;
        City = city;
        State = state;
        ZipCode = zipCode;
        LastFourCreditCardDigits = lastFourCreditCardDigits;
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

    public string StreetAddress { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string LastFourCreditCardDigits { get; }
}