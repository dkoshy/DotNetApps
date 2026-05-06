using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Globomatics.Web.Components;

public class ProductListViewComponent : ViewComponent
{
    private readonly IRepository<Product> _productRepository;
    private readonly ILogger<ProductListViewComponent> _logger;

    public ProductListViewComponent(IRepository<Product> productRepository
         , ILogger<ProductListViewComponent> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        var products = _productRepository.All();
        _logger.LogInformation("ProductListViewComponenet: Fetched {0} number of products", products.Count());
        return Task.FromResult<IViewComponentResult>(View(products));
    } 

}
