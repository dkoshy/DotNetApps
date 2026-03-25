using BethaniesPieShope.Models;
using BethaniesPieShope.Models.Contracts;
using BethaniesPieShope.Repos;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers;

public class OrderController : Controller
{
    private readonly IShoppingCart _shoppingCart;
    private readonly IOrderReopository _orderReopository;

    public OrderController(IShoppingCart shoppingCart,
        IOrderReopository orderReopository)
    {
        _shoppingCart = shoppingCart;
        _orderReopository = orderReopository;
    }
    public IActionResult OrderCheckOut()
    {
        return View();
    }

    [HttpPost]
    public IActionResult OrderCheckOut(OrderViewModel order)
    {
       _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();
        if(_shoppingCart.ShoppingCartItems == null 
            || _shoppingCart.ShoppingCartItems.Count() == 0)
        {
            ModelState.AddModelError("EmptyCart", "Your cart is empty, add some pies first.");
        }

        if (!ModelState.IsValid)
        {
          return View(order);
        }

        var orderdata = new Order
        {
            FirstName = order.FirstName,
            LastName = order.LastName,
            AddressLine1 = order.AddressLine1,
            AddressLine2 = order.AddressLine2,
            City = order.City,
            State = order.State,
            Country = order.Country,
            Pincode = order.Pincode,
            PhoneNumber = order.PhoneNumber,
            Emailid = order.Emailid,
            orderDetails = _shoppingCart.ShoppingCartItems.Select(x => new OrderDetail
            {
                PieId = x.Pie.PieId,
                Amount = x.Amount,
                Price = x.Pie.Price
            }).ToList()
        };
        _orderReopository.CreateOrder(orderdata);
        _shoppingCart.ClearCart();
        return RedirectToAction("OrderCompleted");
    }

    public IActionResult OrderCompleted()
    {
        ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our delicious pies!";
        return View();
    }

}
