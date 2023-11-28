namespace HanksMineralEmporium.Core.SalesManagement.Exception;

/// <summary>
/// Exception thrown when a receipt is not found.
/// </summary>
public class ReceiptNotFoundException : System.Exception
{
    /// <summary>
    /// Creates a new <see cref="ReceiptNotFoundException"/>.
    /// </summary>
    /// <param name="id">The ID of the receipt that was not found.</param>
    public ReceiptNotFoundException(ulong id) : base($"Receipt with ID {id} not found.") {}
}