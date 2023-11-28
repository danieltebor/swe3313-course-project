using HanksMineralEmporium.Data.DatabaseIO.Exception;

namespace HanksMineralEmporium.Core.SalesManagement.Exception;

/// <summary>
/// Exception thrown when a sale is not found.
/// </summary>
public class SaleNotFoundException : System.Exception
{
    /// <summary>
    /// Creates a new <see cref="SaleNotFoundException"/>.
    /// </summary>
    /// <param name="id">The ID of the sale that was not found.</param>
    public SaleNotFoundException(ulong id) : base($"Sale with ID {id} not found.") {}

    /// <summary>
    /// Creates a new <see cref="SaleNotFoundException"/>.
    /// </summary>
    public SaleNotFoundException(DatabaseObjectNotFoundException<ISale> innerException) {
        if (innerException is null)
        {
            throw new ArgumentNullException(nameof(innerException));
        }

        throw new SaleNotFoundException(innerException.Id);
    }
}