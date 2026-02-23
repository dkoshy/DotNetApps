using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models;

namespace BethaniesPieShope.Repos;

public class PieRepository : IPieRepository
{
    private readonly BethaniesPieDbContext _dbContext;

    public PieRepository(BethaniesPieDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Pie> AllPies => _dbContext.Pies.ToList();

    public IEnumerable<Pie> PiesOfTheWeek => _dbContext.Pies.Where(p => p.IsPieOfTheWeek).ToList();

    public Pie? GetPieById(int pieId)
    {
       return _dbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
    }

    public IEnumerable<Pie> SearchPies(string searchQuery)
    {
        throw new NotImplementedException();
    }
}
