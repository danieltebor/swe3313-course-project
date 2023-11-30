using Newtonsoft.Json;

using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Core.SalesManagement.Exception;

namespace HanksMineralEmporium.Service.CheckoutService;

public class CheckoutService
{
    private readonly ISalesManager _salesManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutService(ISalesManager salesManager, IHttpContextAccessor httpContextAccessor)
    {
        _salesManager = salesManager ?? throw new ArgumentNullException(nameof(_salesManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <inheritdoc/>
    public async Task AddItemToCartAsync(IItem item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var sales = await _salesManager.GetAllSalesAsync();
        if (sales.Any(s => s.ItemId == item.Id))
        {
            throw new ItemsAlreadySoldException();
        }

        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cart = (IList<IItem>)GetItemsInCart();
        cart.Add(item);
        httpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    /// <inheritdoc/>
    public void RemoveItemFromCart(IItem item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cart = (List<IItem>)GetItemsInCart();
        cart.RemoveAll(i => i.Id == item.Id);
        httpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    /// <inheritdoc/>
    public IReadOnlyList<IItem> GetItemsInCart()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cartJson = httpContext.Session.GetString("Cart");
        if (cartJson == null)
        {
            return new List<IItem>();
        }

        var cart = JsonConvert.DeserializeObject<List<IItem>>(cartJson)
            ?? new List<IItem>();
        if (cart.Any(i => i is null))
        {
            throw new InvalidOperationException("Cart contains null items.");
        }

        return cart;
    }
}