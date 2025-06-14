using AutoMapper;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using WebMVC.Models;

public class ShoppingCartController : Controller
{
    private readonly IUserService userService;
    private readonly IOrderService orderService;
    private readonly ICouponCodeService couponCodeService;
    private readonly IMapper mapper;
    private readonly IMemoryCache memoryCache;

    public ShoppingCartController(IMapper Mapper, IUserService UserService, IOrderService OrderService, ICouponCodeService couponCodeService, IMemoryCache memoryCache)
    {
        mapper = Mapper;
        userService = UserService;
        orderService = OrderService;
        this.couponCodeService = couponCodeService;
        this.memoryCache = memoryCache;
    }

    public async Task<ActionResult> Index([FromQuery] string? code = null)
    {
        // Attempt to retrieve the cart from session
        List<CartItem> cartItems = GetCartItemsFromSession();

        // If no cart is found in session, create a new empty cart
        if (cartItems == null)
        {
            cartItems = new List<CartItem>();
        }

        ShoppingCartViewModel model = new ShoppingCartViewModel
        {
            CartItems = cartItems
        };

        // If a coupon code is provided, attempt to apply it
        if (code != null)
        {
            var result = await couponCodeService.GetCouponCodeAsync(code);
            if (!result.IsSuccess)
            {
                //ModelState.AddModelError(nameof(ShoppingCartViewModel.CouponCode), result.CustomErrorMessage!);
                model.CouponInfoMessage = result.CustomErrorMessage!;
            }
            else if (result.Data!.Order != null)
            {
                model.CouponInfoMessage = "Coupon code already used";
            }
            else if (result.Data!.GiftCard.ValidFrom > DateTime.Now || result.Data.GiftCard.ValidTo < DateTime.Now)
            {
                model.CouponInfoMessage = $"Coupon code is valid only between {result.Data.GiftCard.ValidFrom} and {result.Data.GiftCard.ValidTo}";
            }
            else
            {
                model.CouponCode = result.Data;
                model.CouponInfoMessage = $"Coupon code applied! Discount €{result.Data.GiftCard.Discount}";
            }
        }

        return View(model);
    }
    // Method to retrieve cart items from session
    private List<CartItem> GetCartItemsFromSession()
    {
        // Retrieve the session data
        var cartData = HttpContext.Session.GetString("CartItems");

        // If no cart data exists, return null
        if (string.IsNullOrEmpty(cartData))
            return null;

        // Deserialize the cart items
        return JsonConvert.DeserializeObject<List<CartItem>>(cartData);
    }

    // Method to save cart items to session
    private void SaveCartItemsToSession(List<CartItem> cartItems)
    {
        // Serialize the cart items and save to session
        var cartData = JsonConvert.SerializeObject(cartItems);
        HttpContext.Session.SetString("CartItems", cartData);
    }
    // Add an item to the cart
    [HttpPost]
    public ActionResult AddToCart(CartItem item)
    {
        // Get the existing cart from session
        List<CartItem> cartItems = GetCartItemsFromSession() ?? new List<CartItem>();


        if (cartItems.Any(i => i.ShopItemId == item.ShopItemId))
        {
            cartItems.Where(i => i.ShopItemId == item.ShopItemId).Single().Count += item.Count;
        }
        else
        {
            cartItems.Add(item);
        }


        // Save the updated cart back to session
        SaveCartItemsToSession(cartItems);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        // Get the current cart from session
        List<CartItem> cartItems = GetCartItemsFromSession() ?? new List<CartItem>();

        // Find the item in the cart with the given id and remove it
        var itemToRemove = cartItems.FirstOrDefault(x => x.ShopItemId == id);
        if (itemToRemove != null)
        {
            cartItems.Remove(itemToRemove);
        }

        // Save the updated cart back to session
        SaveCartItemsToSession(cartItems);

        // Redirect back to the shopping cart page
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(string? code)
    {
        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            // If not authenticated, redirect to the login page
            return RedirectToAction("Login", "Account");
        }

        // could also work just using E-mail, for now getting the userId using userservice
        var Guid = User.Claims.FirstOrDefault().Value;
        var id = await userService.GetUserIdByGuidAsync(Guid);
        List<CartItem> cartItems = GetCartItemsFromSession();


        List<OrderItemDto> orderItems = mapper.Map<List<OrderItemDto>>(cartItems);

        // getting users address from the DB, possible to enable inputting another address
        var addressDisplay = userService.GetUsersDetail().Where(u => u.Id == id).Single().Address;
        var addressDto = mapper.Map<AddressDto>(addressDisplay);

        OrderDto order = new OrderDto
        {
            OrderItems = orderItems,
            UserId = id.Value,
            Address = addressDto,
            CouponCode = code
        };

        var result = await orderService.PlaceWholeOrderAsync(order);
        if (!result.IsSuccess)
        {
            ModelState.AddModelError(nameof(ShoppingCartViewModel), result.CustomErrorMessage!);
            return RedirectToAction("Index");
        }
        // invalidate cache used in OrderOverviewController
        memoryCache.Remove($"orders{id}"); 
        cartItems = new List<CartItem>();
        SaveCartItemsToSession(cartItems);

        return RedirectToAction("Index", "Home");
    }


}
