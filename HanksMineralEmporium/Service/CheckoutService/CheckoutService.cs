using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using HanksMineralEmporium.Core.InventoryManagement;
using HanksMineralEmporium.Core.SalesManagement;
using HanksMineralEmporium.Core.SalesManagement.Exception;

namespace HanksMineralEmporium.Service.CheckoutService;

public class CheckoutService : ICheckoutService
{
    private readonly ISalesManager _salesManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JsonSerializerSettings _jsonSerializerSettings;

    public CheckoutService(ISalesManager salesManager, IHttpContextAccessor httpContextAccessor)
    {
        _salesManager = salesManager ?? throw new ArgumentNullException(nameof(_salesManager));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.All,
        };
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
        httpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, _jsonSerializerSettings));
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
        httpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart, _jsonSerializerSettings));
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

        var cart = JsonConvert.DeserializeObject<List<IItem>>(cartJson, _jsonSerializerSettings)
            ?? new List<IItem>();
        if (cart.Any(i => i is null))
        {
            throw new InvalidOperationException("Cart contains null items.");
        }

        return cart;
    }

    /// <inheritdoc/>
    public bool IsItemInCart(IItem item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        var cart = GetItemsInCart();

        return cart.Any(i => i.Id == item.Id);
    }

    /// <inheritdoc/>
    public void ClearCart()
    {
        var httpContext = _httpContextAccessor.HttpContext
            ?? throw new InvalidOperationException("HttpContext is null.");

        httpContext.Session.SetString("Cart", "");
    }

    /// <inheritdoc/>
    public uint CartItemCount => (uint)GetItemsInCart().Count;
}