using BethaniesPieShope.Models;
using Microsoft.EntityFrameworkCore;

namespace BethaniesPieShope.DBAccess;

public class BethaniesPieDbContext : DbContext
{
    public BethaniesPieDbContext(DbContextOptions<BethaniesPieDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Pie> Pies { get; set; }

}
