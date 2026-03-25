using BethaniesPieShope.Models.Contracts;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Components;

public class ShoppingCartSummary : ViewComponent
{
    private readonly IShoppingCart _shoppingCart;

    public ShoppingCartSummary( IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public IViewComponentResult Invoke()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        var cartView = new ShoppingCartViewModel(_shoppingCart);
        return View(cartView);
    }
}
