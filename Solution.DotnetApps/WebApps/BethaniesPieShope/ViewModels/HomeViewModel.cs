using BethaniesPieShope.Models;

namespace BethaniesPieShope.ViewModels;

public class HomeViewModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<Pie> PiesOfTheWeek { get;}

    public HomeViewModel(IEnumerable<Pie> pies)
    {
        PiesOfTheWeek = pies;
    }
}
