using BethaniesPieShope.Models;
using Microsoft.EntityFrameworkCore;

namespace BethaniesPieShope.DBAccess;

public sealed class BethaniesPieDbContext : DbContext
{

    public BethaniesPieDbContext(DbContextOptions<BethaniesPieDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Pie> Pies { get; set; }
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

}
