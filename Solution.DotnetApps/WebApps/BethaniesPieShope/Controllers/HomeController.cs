using BethaniesPieShope.Repos;
using BethaniesPieShope.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel(_pieRepository.PiesOfTheWeek);
            homeViewModel.Title = "Pies of the week";
            homeViewModel.Description = "Enjoy a weekly selection of our favorite pies";

            return View(homeViewModel);
        }
    }
}
