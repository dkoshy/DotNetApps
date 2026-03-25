using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers;

public class SearchController : Controller
{
    private readonly HttpClient _httpClient;

    public SearchController(IHttpClientFactory httpClientFactory)
    {
       _httpClient = httpClientFactory.CreateClient();
    }

    public IActionResult Index()
    {
        return View();
    }
}
