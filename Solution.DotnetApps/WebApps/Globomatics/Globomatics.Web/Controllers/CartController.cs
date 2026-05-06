using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;

namespace Globomatics.Web.Controllers;

[Route("[Controller]")]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IStateRepository _stateRepository;
    private readonly ILogger<CartController> _logger;

    [ViewData]
    public string Title { get; init; } = "Cart";

    [TempData]
    public string CurrentUpdateStatus { get; set; } = string.Empty;



    public CartController(ICartRepository cartRepository
        , IRepository<Customer> customerRepository
        , IRepository<Order> orderRepository
        , IStateRepository stateRepository
        , ILogger<CartController> logger)
    {
        _cartRepository = cartRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _stateRepository = stateRepository;
        _logger = logger;
    }
    public IActionResult Index(Guid? id)
    {
        return View();
    }

    [HttpPost("add")]
    public IActionResult AddToCart(AddToCartModel addToCartModel)
    {
        if (addToCartModel is null || addToCartModel.Product is null)
            return BadRequest();
       _logger.LogInformation($"Adding Product with id {addToCartModel.Product.ProductId}" +
            $"to cart {addToCartModel.CartId}");
        var cart = _cartRepository.CreateOrUpdate(addToCartModel.CartId
            , addToCartModel.Product.ProductId, addToCartModel.Product.Quantity);
        _cartRepository.SaveChanges();
        _stateRepository.SetValue("NumberOfItems", cart.LineItems.Sum(l => l.Quantity).ToString());
        _stateRepository.SetValue("CartId",cart.CartId.ToString());


        CurrentUpdateStatus = $"Added {addToCartModel.Product.ProductId} to cart.";
        TempData["Theme"] = HttpContext.Request.Cookies["userTheame"] ?? "Dark";

        return RedirectToAction("Index");
    }

    [HttpPost("update")]
    [ValidateAntiForgeryToken]
    public IActionResult Update(UpdateQuantitiesModel updateQuantitiesModel)
    {
        if (updateQuantitiesModel.Products is null)
        {
            return BadRequest();
        }
        Cart cart = null!;
        updateQuantitiesModel?.Products.ToList().ForEach(p =>
        {
            cart = _cartRepository.CreateOrUpdate(updateQuantitiesModel.CartId, p.ProductId, p.Quantity);
            _logger.LogInformation($"Adding products {p.ProductId} to cart {updateQuantitiesModel.CartId}");
        });
        _cartRepository.SaveChanges();
        _stateRepository.SetValue("NumberOfItems", cart.LineItems.Sum(l => l.Quantity).ToString());
        _stateRepository.SetValue("CartId", cart.CartId.ToString());
        //CurrentUpdateStatus = "Cart updated successfully.";
        _logger.LogInformation($"Last Status:- {CurrentUpdateStatus}");
        return RedirectToAction("Index", "Cart");
    }

    [HttpPost("Finalize")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateOrderModel createOrderModel)
    {
        if (createOrderModel.Customer is null)
        {
            ModelState.AddModelError(nameof(createOrderModel.Customer), "Customer data is not available.");
            return View();
        }

        if (createOrderModel.Customer.Name.Length <= 2)
        {
            ModelState.AddModelError(nameof(createOrderModel.Customer.Name), "Customer name is too short.");
            return View();
        }
        if (!ModelState.IsValid)
        {
            return View();
        }

        var customer = new Customer
        {
            Email = createOrderModel.Customer.Email,
            Name = createOrderModel.Customer.Name,
            City = createOrderModel.Customer.City,
            Country = createOrderModel.Customer.Country,
            ShippingAddress = createOrderModel.Customer.ShippingAddress,
            PostalCode = createOrderModel.Customer.PostalCode,
        };
        _logger.LogInformation($"Creating a new order for {customer.CustomerId}");
        _customerRepository.Add(customer);
        var order = new Order
        {
            CustomerId = customer.CustomerId
        };

        if (createOrderModel.CartId is null || createOrderModel.CartId == Guid.Empty)
        {
            ModelState.AddModelError("Cart", "Cart has been deleted");

            return View("Index");
        }

        var cart = _cartRepository.Get(createOrderModel.CartId.Value);

        if (cart is null)
        {
            ModelState.AddModelError("Cart", "Cart has been deleted");

            return View("Index");
        }
        order.LineItems.AddRange(cart.LineItems);
        _orderRepository.Add(order);
        _cartRepository.Update(cart);
        _cartRepository.SaveChanges();
        _logger.LogInformation($"Order {order.OrderId} created successfully for customer {customer.CustomerId}");
        _stateRepository.Remove("NumberOfItems");
        _stateRepository.Remove("CartId");
        return RedirectToAction("ThankYou");
    }

    [Route("thankYou")]
    public IActionResult ThankYou()
    {
        return View();
    }
}
