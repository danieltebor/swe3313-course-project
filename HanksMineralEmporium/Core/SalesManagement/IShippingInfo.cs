namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Contract for an object that stores shipping information.
/// </summary>
public interface IShippingInfo
{
    public string StreetAddress { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public ShippingOption SelectedShippingOption { get; }
}