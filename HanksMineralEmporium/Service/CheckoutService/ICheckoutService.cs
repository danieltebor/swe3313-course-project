using HanksMineralEmporium.Core.InventoryManagement;

namespace HanksMineralEmporium.Service.CheckoutService;

public interface ICheckoutService
{
    /// <summary>
    /// Adds an item to the cart.
    /// </summary>
    /// <param name="item">The item to add to the cart.</param>
    /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    /// <exception cref="ItemAlreadySoldException">Thrown when the item is already sold.</exception>
    public Task AddItemToCartAsync(IItem item);

    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="item">The item to remove from the cart.</param>
    /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public Task RemoveItemFromCartAsync(IItem item);

    /// <summary>
    /// Gets the items in the cart.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    /// <returns>The items in the cart.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public Task<IReadOnlyList<IItem>> GetItemsInCartAsync();
}