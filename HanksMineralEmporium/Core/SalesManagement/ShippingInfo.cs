namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Implements the <see cref="IShippingInfo"/> interface.
/// </summary>
internal class ShippingInfo : IShippingInfo
{
    public string StreetAddress { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public ShippingOption SelectedShippingOption { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShippingInfo"/> class.
    /// </summary>
    /// <param name="streetAddress">The street address.</param>
    /// <param name="city">The city.</param>
    /// <param name="state">The state.</param>
    /// <param name="zipCode">The zip code.</param>
    /// <param name="selectedShippingOption">The selected shipping option.</param>
    /// <exception cref="ArgumentException">Thrown when any of the parameters null or are empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when any of the parameters are null.</exception>
    public ShippingInfo(string streetAddress, string city, string state, string zipCode, ShippingOption selectedShippingOption)
    {
        if (string.IsNullOrWhiteSpace(streetAddress))
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

        StreetAddress = streetAddress;
        City = city;
        State = state;
        ZipCode = zipCode;
        SelectedShippingOption = selectedShippingOption;
    }
}