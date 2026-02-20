using Books.API.Entities;

namespace Books.API.DataAccess;

public interface IBookRepository
{

    Task<Book?> GetBookByIdAsync(Guid id);
    Task<IEnumerable<Book>> GetAllAsync(IEnumerable<Guid>? ids = null);
    Task<BookCover?> GetBookCoverAsync(Guid id);
    Task<IEnumerable<BookCover>> GetBookCoversProcessAfterWaitForAllAsync(Guid guid);
    Task<IEnumerable<BookCover>> GetBookCoversProcessOneByOneAsync(Guid guid, CancellationToken token);
    void AddBook(Book book);
}
