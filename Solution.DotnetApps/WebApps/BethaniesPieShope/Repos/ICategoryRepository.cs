using BethaniesPieShope.Models;

namespace BethaniesPieShope.Repos;

public interface ICategoryRepository
{
    IEnumerable<Category> AllCategories { get; }
}