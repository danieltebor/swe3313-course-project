namespace HanksMineralEmporium.Core.SalesManagement;

/// <summary>
/// Defines the contract for a credit card info.
/// </summary>
public interface ICreditCardInfo
{
    public string CardNumber { get; }
    public string ExpirationDate { get; }
    public string CSV { get; }
}