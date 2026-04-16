using Microsoft.EntityFrameworkCore;
using PublisherDomain;

namespace PublisherDataAccess
{
    public  class PubDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=PublisherDB;Username=postgres;Password=your_password");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
