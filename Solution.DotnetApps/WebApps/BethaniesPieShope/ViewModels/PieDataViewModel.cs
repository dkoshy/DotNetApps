using BethaniesPieShope.Models;

namespace BethaniesPieShope.ViewModels;

public class PieDataViewModel
{
    public PieDataViewModel()
    {
        Pies = new List<Pie>();
    }
    public IEnumerable<Pie> Pies { get; set; }
    public string CurrentCategory { get; set; } = string.Empty;
}
