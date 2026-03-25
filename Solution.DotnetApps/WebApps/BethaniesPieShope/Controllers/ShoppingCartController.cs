using BethaniesPieShope.Models.Contracts;
using BethaniesPieShope.Repos;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IShoppingCart _shoppingCart;
    private readonly IPieRepository _pieRepository;

    public ShoppingCartController(IShoppingCart shoppingCart
        ,IPieRepository pieRepository)
    {
        _shoppingCart = shoppingCart;
        _pieRepository = pieRepository;
    }

    public IActionResult Index()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;
        var cartView = new ShoppingCartViewModel(_shoppingCart);

        return View(cartView);
    }

   public IActionResult AddToCart(int pieId)
    {
       var selectedPie = _pieRepository.GetPieById(pieId);
       if(selectedPie != null)
        {
            _shoppingCart.AddtoCart(selectedPie);
        }

       return RedirectToAction("Index");
    }
    public IActionResult RemoveFromCart(int pieId)
    {
        var pieToRemove = _pieRepository.GetPieById(pieId);
        if (pieToRemove != null)
        { 
            _shoppingCart.RemoveFromCart(pieToRemove);
        }
        return RedirectToAction("List", "Pie");
    }
}
