using Microsoft.EntityFrameworkCore;
using PublisherDataAccess;
using PublisherDomain;

var dataContext = new PubDBContext();

//dataContext.Database.EnsureCreated();
//var isConnected = await dataContext.Database.CanConnectAsync();
//Console.WriteLine(isConnected);

//InserAuthor();
//InsertingMultipleAuthors();
//RetriveMutipleRowsandUpdate();
//CoordinateRetriveandUpdateAuthor();
//DeleteAnAuthor();
//await FindData();


//InsertBooksForAuthor();
//EagerLoadingWithInclude();
//EagerloadingWithFilterInclude();
//LoadingRelatedDataWithProjection();
//LoadBookOfalredyLoadedAuthor();
//LoadAuthorforInMemoryBook();
//modifyingRelateddataInMemeory();
//UpdateusingEntityEntry();
//CretaeArtistAndCover();
//AddaNewCoverToArtist();
//FetchArtistWhohasAltestOnecover();
//FetchArtistwithCoverDetails();
UnassignArtistFromCover();


void InserAuthor()
{
    var author = new Author { FirstName = "Frank", LastName = "Herbert" };
    dataContext.Authors.Add(author);
    dataContext.SaveChanges();
}

void InsertingMultipleAuthors()
{
    var authors = new List<Author>
    {
        new Author { FirstName = "Frank", LastName = "Herbert" },
        new Author { FirstName = "George", LastName = "Orwell" },
        new Author { FirstName = "Julie", LastName = "Lerman" },
        new Author { FirstName = "Julia", LastName = "Lerman" }
    };
    dataContext.Authors.AddRange(authors);
    dataContext.SaveChanges();
}

void RetriveMutipleRowsandUpdate()
{
    var authors = dataContext.Authors.Where(a => a.LastName == "Lerman").ToList();
    foreach (var author in authors)
    {
        author.LastName = "Lehrman";
    }
    Console.WriteLine(dataContext.ChangeTracker.DebugView.ShortView);
    dataContext.ChangeTracker.DetectChanges();
    Console.WriteLine(dataContext.ChangeTracker.DebugView.ShortView);

    dataContext.SaveChanges();
}

// disconnected scenario.

void CoordinateRetriveandUpdateAuthor()
{   
    var author = FindAnAuthor();
    if (author is not null)
    {
        author.LastName = "Herbert Jr.";
        SaveAnAuthor(author);
    }
}

void SaveAnAuthor(Author author)
{
    using var context = new PubDBContext();
    context.Authors.Update(author);
    Console.WriteLine(context.ChangeTracker.DebugView.ShortView);
    context.SaveChanges();
}

Author? FindAnAuthor()
{
    using var context = new PubDBContext();
    var author = context.Authors.FirstOrDefault(a => a.LastName == "Herbert");
    return author;
}


//delete Author

void DeleteAnAuthor()
{
    var author = dataContext.Find<Author>(2);
    if(author is not null)
    {
        dataContext.Authors.Remove(author);
        Console.WriteLine(dataContext.ChangeTracker.DebugView.ShortView);
        dataContext.SaveChanges();
    }
}

// find Data
async Task FindData()
{
    var author = await dataContext.Authors.FindAsync(2);
    Console.WriteLine(author?.FirstName);

}

//inserting mutiple books for an author

void InsertBooksForAuthor()
{
    var author = new Author { FirstName="Don", LastName="Quixote"};
    var books = new List<Book>
    {
        new Book { Title="The Ingenious Gentleman Don Quixote of La Mancha", BasePrice=15.50m , PublishDate=DateTime.UtcNow},
        new Book { Title="Don Quixote", BasePrice=20.25m,PublishDate=new DateTime(1999,10,24).ToUniversalTime()}
    };
    author.Books.AddRange(books);
    dataContext.Authors.Add(author);
    dataContext.SaveChanges();
}

//eager loading with include

void EagerLoadingWithInclude()
{
    var author = dataContext
                    .Authors.Where(a => a.LastName == "Quixote")
                    .Include(a => a.Books)
                    .FirstOrDefault();
    Console.WriteLine($"Author {author?.FirstName} written {author?.Books.Count} Books");

    
}

void EagerloadingWithFilterInclude()
{
    //filtering related data with include

    var authorwithFilteredBooks = dataContext.Authors.Where(a => a.LastName == "Quixote")
                                  .Include(a => a.Books.Where(b => b.BasePrice > 20m))
                                  .FirstOrDefault();

    Console.WriteLine($"Author {authorwithFilteredBooks?.FirstName} has written " +
                  $"{authorwithFilteredBooks?.Books.Count} books whose price is more than 20 Dollar");
    // Include can be used multiple times to fetch related content.
    //TenInclude can be used to fetch multiple levels of related content.
}


// Loading Related data with Projection

void LoadingRelatedDataWithProjection()
{
    var author = dataContext.Authors.Where(a => EF.Functions.ILike(a.LastName, "%koshy%"))
                 .Select(a => new
                 {
                     Name = $"{a.LastName},{a.FirstName}",
                     Books = a.Books.Where(b => b.BasePrice > 10)
                              .Select(b => new {  b.Title, b.BasePrice })
                              .ToList()
                 }).ToList();

      author.ForEach(a => {
          Console.WriteLine($"Author {a?.Name} has written {a?.Books?.Count} books with a base price greater than 10.");

      });

                  
}

//Loading related data of in-memeory entities

void LoadBookOfalredyLoadedAuthor()
{
    var author = dataContext.Authors.Where(a => a.LastName == "Quixote").FirstOrDefault();

    if (author is not null)
    {
      //dataContext.Entry(author).Collection(a => a.Books).Load();
      dataContext.Entry(author).Collection(a => a.Books)
                 .Query().Where(b => EF.Functions.ILike(b.Title,"%la%")).Load();
        Console.WriteLine($"Author {author.FirstName} has written {author.Books.Count} Book(s).");
    }
}


void LoadAuthorforInMemoryBook()
{
    var book = dataContext.Books.Where(b => b.Title == "Dune").FirstOrDefault();
    if (book is not null)
    {
        dataContext.Entry(book).Reference(b => b.Author).Load();
        Console.WriteLine($"Book {book.Title} is written by {book.Author?.FirstName} {book.Author?.LastName}");
    }
}

//modyfing related data of in-memory entities
void modifyingRelateddataInMemeory()
{
    var author = dataContext.Authors.Where(a => EF.Functions.ILike(a.LastName, "%koshy%"))
                      .Include(a => a.Books)
                      .ToList();
    var deepak = author.Where(a => a.FirstName == "Deepak").FirstOrDefault();
    var thomas = author.Where(a => a.FirstName == "Thomas").FirstOrDefault();

    if (deepak is not null)
    {
       var book1 = deepak.Books.Where(b => b.Title == "Nmukku Parkkam").FirstOrDefault();
       var book2 = deepak.Books.Where(b => b.Title == "Musirikal Pookumbol").FirstOrDefault();
        deepak.Books.Remove(book1);
        book2.Title = "Muthirikal Pookumbol";
        book2.AuthorFK = thomas?.Id;
    }
    dataContext.SaveChanges();
}

//Entity Etry object 

void UpdateusingEntityEntry()
{
    var book = FetchOneBook();
    UpdateOneBook(book);
}
Book FetchOneBook()
{
    var ctx = new PubDBContext();
    var data = ctx.Books.Find(8);
    return data;
}

void UpdateOneBook(Book book)
{
    var ctx = new PubDBContext();
    book.AuthorFK = 5;
    ctx.Entry(book).State = EntityState.Modified;
    ctx.SaveChanges();
}


//many to many Relation
void CretaeArtistAndCover()
{
    var artist1 = dataContext.Artists.Find(1);
    var artist2 = dataContext.Artists.Find(2);
    var cover = dataContext.Covers.Find(1);

    cover?.Artists.Add(artist1);
    cover?.Artists.Add(artist2);
    dataContext.SaveChanges();
}

void AddaNewCoverToArtist()
{
    var asrtist = dataContext.Artists.Find(1);
    var cover = new Cover { DesignIdeas = "author has an amazing phot" };
    asrtist?.Covers.Add(cover);
    dataContext.SaveChanges();
}

//fetching many to many related data
void FetchArtistWhohasAltestOnecover()
{
    var artists = dataContext.Artists.Where(a => a.Covers.Any()).ToList();
    Console.WriteLine($"We have {artists.Count} Artist who desined covers for book");
}

void  FetchArtistwithCoverDetails()
{
    var artists = dataContext.Artists
                   .Include(c => c.Covers)
                   .ToList();
    Console.WriteLine($"we have {artists.Count} Artist");
}

void UnassignArtistFromCover()
{
   var cover = dataContext.Covers.Where(c => c.CoverId == 1)
        .Include(c => c.Artists.OrderBy(a=>a.ArtistId))
        .FirstOrDefault();
    cover?.Artists.Remove(cover.Artists.Find(a => a.ArtistId == 1)?? new Artist());
    dataContext.SaveChanges();
}