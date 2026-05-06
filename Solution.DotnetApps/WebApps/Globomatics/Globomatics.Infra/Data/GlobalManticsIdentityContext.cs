using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Globomatics.Infra.Data;

public class GlobalManticsIdentityContext : IdentityDbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Host=localhost;Port=5432;Database=Globomantics;Username=postgres;Password=postgres;";
            optionsBuilder.UseNpgsql(connectionString);

        }
    }

}
