using Microsoft.AspNetCore.Mvc;

namespace BethaniesPieShope.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
