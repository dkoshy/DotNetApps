using BethaniesPieShope.Repos;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers;

public class PieController : Controller
{
    private readonly IPieRepository _pieRepository;

    public PieController(IPieRepository pieRepository)
    {
        _pieRepository = pieRepository;
    }
    public IActionResult List()
    {
        var data =  new PieDataViewModel
        {
            Pies = _pieRepository.AllPies,
            CurrentCategory = "Chees Pie"
        };

        return View(data);
    }
}
