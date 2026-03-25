using BethaniesPieShope.Models.Contracts;

namespace BethaniesPieShope.ViewModels;

public class ShoppingCartViewModel
{

    public IShoppingCart ShoppingCart { get;}
    public decimal ShoppingCartTotal { get;  }
    public decimal ShoppingCartCount { get;  }

    public ShoppingCartViewModel(IShoppingCart shoppingCart )
    {
        ShoppingCart = shoppingCart;
        ShoppingCartTotal = shoppingCart.GetShoppingCartTotal();
        ShoppingCartCount = shoppingCart.GetShoppingCartItems().Select(c => c.Amount).Sum();
    }
}
