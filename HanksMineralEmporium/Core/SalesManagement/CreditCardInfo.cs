namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Implements the <see cref="ICreditCardInfo"/> interface.
/// </summary>
internal class CreditCardInfo : ICreditCardInfo
{
    public string CardNumber { get; }
    public string ExpirationDate { get; }
    public string CSV { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CreditCardInfo"/> class.
    /// </summary>
    /// <param name="cardNumber">The card number.</param>
    /// <param name="expirationDate">The expiration date.</param>
    /// <param name="csv">The CSV.</param>
    /// <exception cref="ArgumentException">Thrown when any of the parameters null or are empty.</exception>
    public CreditCardInfo(string cardNumber, string expirationDate, string csv)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
        {
            throw new ArgumentException("Card Number cannot be null or empty.", nameof(cardNumber));
        }
        else if (string.IsNullOrWhiteSpace(expirationDate))
        {
            throw new ArgumentException("Expiration Date cannot be null or empty.", nameof(expirationDate));
        }
        else if (string.IsNullOrWhiteSpace(csv))
        {
            throw new ArgumentException("CSV cannot be null or empty.", nameof(csv));
        }

        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        CSV = csv;
    }
}