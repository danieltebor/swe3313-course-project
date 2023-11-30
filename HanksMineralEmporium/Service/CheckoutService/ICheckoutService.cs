using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement;

namespace HanksMineralEmporium.Service.CheckoutService;

public interface ICheckoutService
{
    /// <summary>
    /// Gets the number of items in the cart.
    /// </summary>
    /// <returns>The number of items in the cart.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public uint CartItemCount { get; }

    /// <summary>
    /// Stores the shipping info.
    /// </summary>
    public IShippingInfo? ShippingInfo { get; set; }

    /// <summary>
    /// Stores the credit card info.
    /// </summary>
    public ICreditCardInfo? CreditCardInfo { get; set; }

    /// <summary>
    /// Adds an item to the cart.
    /// </summary>
    /// <param name="item">The item to add to the cart.</param>
    /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    /// <exception cref="ItemsAlreadySoldException">Thrown when the item is already sold.</exception>
    public Task AddItemToCartAsync(IItem item);

    /// <summary>
    /// Removes an item from the cart.
    /// </summary>
    /// <param name="item">The item to remove from the cart.</param>
    /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public void RemoveItemFromCart(IItem item);

    /// <summary>
    /// Gets the items in the cart.
    /// </summary>
    /// <returns>The items in the cart.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public IReadOnlyList<IItem> GetItemsInCart();

    /// <summary>
    /// Checks if an item is in the cart.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>True if the item is in the cart, false otherwise.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public bool IsItemInCart(IItem item);

    /// <summary>
    /// Clears the cart.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when the HttpContext is null.</exception>
    public void ClearCart();
}