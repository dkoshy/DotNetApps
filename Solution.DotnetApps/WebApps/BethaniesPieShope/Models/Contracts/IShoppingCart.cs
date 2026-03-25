namespace BethaniesPieShope.Models.Contracts;

public interface IShoppingCart
{
    string? ShoppingCartId { get; set; }
    public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

    void AddtoCart(Pie pie);
    IEnumerable<ShoppingCartItem> GetShoppingCartItems();
    decimal GetShoppingCartTotal();
    int RemoveFromCart(Pie pie);
    void ClearCart();
}