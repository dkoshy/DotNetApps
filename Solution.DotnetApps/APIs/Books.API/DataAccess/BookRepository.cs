using Books.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Books.API.DataAccess;

public class BookRepository : BaseRepo, IBookRepository
{
    private readonly HttpClient _httpClient;

    public BookRepository(BookManagementDbContext dbContext
        , IHttpClientFactory httpClient)
        :base(dbContext)
    {
        _httpClient = httpClient?.CreateClient() ?? throw new ArgumentNullException();
    }

    public  IAsyncEnumerable<Book> GetBookStream()
    {
        return  _dbContext.Books.AsAsyncEnumerable();
    }
    public void AddMultiple(IEnumerable<Book> books)
    {
        _dbContext.Books.AddRange(books);
    }
    public void AddBook(Book book)
    {
        _dbContext.Books.Add(book);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(IEnumerable<Guid>? guids = null)
    {
        if (guids != null && guids.Any()) 
        {
          return await  _dbContext.Books
                .Include(b => b.Author)
                .Where(b => guids.Any(i => i == b.Id))
                .ToListAsync();
        }

        return await _dbContext.Books
                      .Include(b => b.Author)
                      .ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await _dbContext.Books
                       .Include (b => b.Author)
                       .FirstOrDefaultAsync(b => b.Id == id);
                      
    }

    public async Task<BookCover?> GetBookCoverAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://localhost:52644/api/bookcovers/{id}");
            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<BookCover>(
                   await response.Content.ReadAsStringAsync(),
                   new JsonSerializerOptions(JsonSerializerDefaults.Web));
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<IEnumerable<BookCover>> GetBookCoversProcessAfterWaitForAllAsync(Guid guid)
    {
        var bookCoverUrls = new[]
        {
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover1",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover2",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover3",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover4",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover5"
        };
        var response = new List<BookCover>();
        var tasks = new List<Task<HttpResponseMessage>>();
        foreach (var url in bookCoverUrls)
        {
            tasks.Add(_httpClient.GetAsync(url));
        }
        var completedTaskResult =  await Task.WhenAll(tasks);

        foreach (var result in completedTaskResult.Reverse())
        {
          var content =   JsonSerializer.Deserialize<BookCover>(await result.Content.ReadAsStringAsync()
               , new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (content != null)
            {
                response.Add(content);
            }
        }
        return response;
    }

    public async Task<IEnumerable<BookCover>> GetBookCoversProcessOneByOneAsync(Guid guid,
        CancellationToken cancellationIncommingToken)
    {
        var bookCoverUrls = new[]
      {
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover1",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover2",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover3",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover4",
                $"http://localhost:52644/api/bookcovers/{guid}-dummycover5"
        };
        var data = new List<BookCover>();

        using var cancelationInternalTokenSorce = new CancellationTokenSource();

        using var likedCancelationTokenSouce = CancellationTokenSource.CreateLinkedTokenSource(
            cancelationInternalTokenSorce.Token, cancellationIncommingToken);

        foreach (var url in bookCoverUrls)
        {
            var response = await _httpClient.GetAsync(url, likedCancelationTokenSouce.Token);
            if(response.IsSuccessStatusCode)
            {
                var content = JsonSerializer.Deserialize<BookCover>(await response.Content.ReadAsStringAsync(likedCancelationTokenSouce.Token)
                    ,new JsonSerializerOptions(JsonSerializerDefaults.Web));
                if (content != null)
                {
                    data.Add(content);
                }
            }
            else
            {
                cancelationInternalTokenSorce.Cancel();
            }
        }
        return data;
    }

}