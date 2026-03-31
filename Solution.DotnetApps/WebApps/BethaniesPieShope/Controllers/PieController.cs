using BethaniesPieShope.Repos;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers;

public class PieController : Controller
{
    private readonly IPieRepository _pieRepository;
    private readonly HttpClient _htppclient;

    public PieController(IPieRepository pieRepository,
        IHttpClientFactory clientFactory)
    {
        _pieRepository = pieRepository;
        _htppclient = clientFactory.CreateClient();
    }
    public IActionResult List(string category)
    {
        PieDataViewModel data = default!;

        if (category == null)
        {
            data = new PieDataViewModel
            {
                Pies = _pieRepository.AllPies,
                CurrentCategory = "All Pies"
            };
        }
        else
        {
            data = new PieDataViewModel
            {
                Pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category),
                CurrentCategory = category
            };
        }

        return View(data);
    }

    public IActionResult Details(int id)
    {
        var pie = _pieRepository.GetPieById(id);
        if (pie == null)
            return NotFound();
        return View(pie);
    }

    public IActionResult Search()
    {
        return View();
    }

}
