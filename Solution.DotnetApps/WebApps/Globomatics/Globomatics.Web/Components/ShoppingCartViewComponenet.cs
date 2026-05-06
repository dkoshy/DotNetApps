using Globomantics.Infrastructure.Data;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Globomatics.Web.Components;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly GlobomanticsContext _globomanticsContext;
    private readonly IStateRepository _stateRepository;

    public ShoppingCartViewComponent(GlobomanticsContext globomanticsContext,
        IStateRepository stateRepository)
    {
        _globomanticsContext = globomanticsContext;
        _stateRepository = stateRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(string CartId, bool IsCompact)
    {
        if (!Guid.TryParse(CartId, out var cartId))
        {
            return View(new ShoppingCartModel { IsCompact = IsCompact });
        }
        var cart = await _globomanticsContext.Carts
                      .Include(x => x.LineItems)
                      .ThenInclude(x => x.Product)
                      .FirstOrDefaultAsync(x => x.CartId == cartId);
        if (cart is not null)
        {
            _stateRepository.SetValue("NumberOfItems"
                , cart.LineItems.Sum(l => l.Quantity).ToString());
            _stateRepository.SetValue("CartId",
               cart.CartId.ToString());
        }
        return View(new ShoppingCartModel { Cart = cart, IsCompact = IsCompact });

    }

}
