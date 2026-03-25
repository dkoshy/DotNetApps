using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BethaniesPieShope.Models;

public class ShoppingCart : IShoppingCart
{
    private readonly BethaniesPieDbContext _dbcontext;
    public string? ShoppingCartId { get; set; }
    public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    public ShoppingCart(BethaniesPieDbContext context)
    {
        _dbcontext = context;
    }
   
    public static ShoppingCart CreateACart(IServiceProvider services)
    {
        var session = services.GetRequiredService<IHttpContextAccessor>()
                        ?.HttpContext?.Session;
        var dbcontext = services.GetService<BethaniesPieDbContext>() ?? throw new Exception("Error initializing the cart");
        var shoppingcartId = session?.GetString("CardId") ?? Guid.NewGuid().ToString();
        session?.SetString("CardId", shoppingcartId);
        var shoppingCart = new ShoppingCart(dbcontext)
        {
            ShoppingCartId = shoppingcartId
        };

        return shoppingCart;
    }

    public void AddtoCart(Pie pie)
    {
        var pieinCart = _dbcontext.ShoppingCartItems.SingleOrDefault(i =>
                       i.Pie.PieId == pie.PieId && i.ShoppingCartId == ShoppingCartId);
        if (pieinCart == null)
        {
            var newitem = new ShoppingCartItem
            {
                Pie = pie,
                Amount = 1,
                ShoppingCartId = this.ShoppingCartId
            };
            _dbcontext.ShoppingCartItems.Add(newitem);
        }
        else
        {
            pieinCart.Amount++;
        }
        _dbcontext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var pietobeRemoved = _dbcontext.ShoppingCartItems.SingleOrDefault(i =>
                    i.Pie.PieId == pie.PieId && i.ShoppingCartId == ShoppingCartId);
        int localCartAmount = 0;
        if (pietobeRemoved != null)
        {
            if (pietobeRemoved.Amount > 1)
            {
                pietobeRemoved.Amount--;
                localCartAmount = pietobeRemoved.Amount;
            }
            else
            {
                _dbcontext.ShoppingCartItems.Remove(pietobeRemoved);
            }
        }
        _dbcontext.SaveChanges();
        return localCartAmount;
    }

    public IEnumerable<ShoppingCartItem> GetShoppingCartItems()
    {
        return   ShoppingCartItems ??  _dbcontext.ShoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCartId)
            .Include(p => p.Pie)
            .ToList();
    }

    public void ClearCart()
    {
        var cartItems = _dbcontext.ShoppingCartItems.Where(p => p.ShoppingCartId == ShoppingCartId)
                          .ToList();
        _dbcontext.ShoppingCartItems.RemoveRange(cartItems);
        _dbcontext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        return _dbcontext.ShoppingCartItems
                    .Where(p => p.ShoppingCartId == ShoppingCartId)
                    .Select(p => p.Pie.Price * p.Amount).Sum();
    }
}
