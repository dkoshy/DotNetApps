using BethaniesPieShope.DBAccess;
using BethaniesPieShope.Models;

namespace BethaniesPieShope.Repos;

public class CategoryRepository : ICategoryRepository
{
    private readonly BethaniesPieDbContext _dbContext;

    public CategoryRepository(BethaniesPieDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Category> AllCategories =>  _dbContext.Categories.ToList();
}
