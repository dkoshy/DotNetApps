using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using PublisherDataAccess.Migrations;

namespace PublisherDataAccess
{
    public class PubDBContext : DbContext
    {
        //private StreamWriter _logStreamWriter = new StreamWriter("efcore_log.txt", append: true)
        //{
        //    AutoFlush = true
        //};
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=localhost;port=5432;Database=PublisherDB;Username=postgres;Password=postgres;Timeout=10;sslmode=prefer")
                          .LogTo(Console.WriteLine, new string[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                          .EnableSensitiveDataLogging();
            //.LogTo(_logStreamWriter.WriteLine, new string[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            //.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entity Configurations
            modelBuilder.Entity<Book>()
                .Property(b => b.AuthorFK)
                .HasColumnName("AuthorId").IsRequired(false);


            //Entity Relationships
            modelBuilder.Entity<Book>()
                 .HasOne(b => b.Author)
                 .WithMany(a => a.Books)
                 .HasForeignKey(b => b.AuthorFK)
                 .IsRequired(false);

            //seed data for authors
            var authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "Frank", LastName = "Herbert" },
                new Author { Id = 2, FirstName = "George", LastName = "Orwell" },
                new Author { Id = 3, FirstName = "Julie", LastName = "Lerman" },
                new Author { Id = 4, FirstName = "Julia", LastName = "Lerman" },
                new Author { Id = 5, FirstName = "Deepak", LastName = "Koshy" },
                new Author { Id = 6, FirstName = "Bini", LastName = "Koshy" },
                new Author { Id = 7, FirstName = "Thomas", LastName = "Koshy" },
            };

            modelBuilder.Entity<Author>().HasData(authors);

            //Seed some book data
            var books = new List<Book>
             {
                 new Book { BookId = 1, Title = "Dune", PublishDate = new DateTime(1965, 8, 1).ToUniversalTime(), BasePrice = 9.99m, AuthorFK = 1 },
                 new Book { BookId = 2, Title = "1984", PublishDate = new DateTime(1949, 6, 8).ToUniversalTime(), BasePrice = 7.99m, AuthorFK = 2 }
             };

            modelBuilder.Entity<Book>().HasData(books);

            //seed artist and book
            modelBuilder.Entity<Artist>().HasData(new List<Artist>
            {
                new Artist {ArtistId = 1, FirstName = "Pablo", LastName="Picasso"},
                new Artist {ArtistId = 2, FirstName = "Dee", LastName="Bell"},
                new Artist {ArtistId = 3, FirstName ="Katharine", LastName="Kuharic"}
            });

            modelBuilder.Entity<Cover>().HasData(new List<Cover>
            {
                new Cover {CoverId = 1, DesignIdeas="How about a left hand in the dark?", DigitalOnly=false},
                new Cover {CoverId = 2, DesignIdeas= "Should we put a clock?", DigitalOnly=true},
                new Cover {CoverId = 3, DesignIdeas="A big ear in the clouds?", DigitalOnly = false}
            });

        }
        //public override void Dispose()
        //{
        //    _logStreamWriter.Dispose();
        //    base.Dispose();
        //}
    }
}
