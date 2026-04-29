using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Globomatics.Web.Controllers;

public class HomeController : Controller
{
    private readonly IRepository<Product> _productRepositiry;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IRepository<Product> productRepositiry
                      , ILogger<HomeController> logger)
    {
        _productRepositiry = productRepositiry;
        _logger = logger;
    }
    public IActionResult Index()
    {
        var products = _productRepositiry.All();
        _logger.LogInformation($"Fetched {products.Count()} Products");
        return View(products);
    }

    [Route("details/{productId:guid}/{slug}")]
    public IActionResult TicketDetails(Guid productId
        ,[RegularExpression(@"[a-zA-Z0-9- ]+$")] string? slug)
    {
        if (ModelState.IsValid is false)
        {
            return BadRequest();
        }
        var product = _productRepositiry.Get(productId);
        if (product is null)
        {
            return NotFound();
        }
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}