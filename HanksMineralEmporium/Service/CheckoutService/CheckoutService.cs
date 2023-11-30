using Newtonsoft.Json;

using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Core.SalesManagement.Exception;

namespace HanksMineralEmporium.Service.CheckoutService;

public class CheckoutService
{
    private readonly ISalesManager _salesManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CheckoutService(ISalesManager _salesManager, IHttpContextAccessor httpContextAccessor)
    {
        _salesManager = _salesManager ?? throw new ArgumentNullException(nameof(_salesManager));
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
        if (sales.Any(s => s.Item.Id == item.Id))
        {
            throw new ItemAlreadySoldException("Item is already sold.");
        }

        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cart = await GetItemsInCartAsync();
        cart.Add(item);
        httpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    /// <inheritdoc/>
    public async Task RemoveItemFromCartAsync(IItem item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cart = await GetCartAsync();
        cart.RemoveAll(i => i.Id == item.Id);
        httpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<IItem>> GetItemsInCartAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cart = await JsonConvert.DeserializeObject<List<IItem>>(httpContext.Session.GetString("Cart"))
            ?? new List<IItem>();
    }
}