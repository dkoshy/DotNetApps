using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models;
using Microsoft.EntityFrameworkCore;

namespace BethaniesPieShope.Repos;

public class PieRepository : IPieRepository
{
    private readonly BethaniesPieDbContext _dbContext;

    public PieRepository(BethaniesPieDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Pie> AllPies => _dbContext.Pies
                                            .Include(p => p.Category).ToList();
    public IEnumerable<Pie> PiesOfTheWeek => _dbContext.Pies.Where(p => p.IsPieOfTheWeek).ToList();

    public Pie? GetPieById(int pieId)
    {
        return _dbContext.Pies.Include(p => p.Category).FirstOrDefault(p => p.PieId == pieId);
    }

    public IEnumerable<Pie> SearchPies(string searchQuery)
    {
        if (string.IsNullOrEmpty(searchQuery))
        {
            return new List<Pie>();
        }

        return _dbContext.Pies
            .Include(p => p.Category)
            .Where(p => EF.Functions.ILike(p.Name,$"%{searchQuery}%")
                        || (p.LongDescription != null
                        && EF.Functions.ILike(p.LongDescription, $"%{searchQuery}%") )).ToList();
    }
}
