namespace Books.API.Models;

public class BookWithCoverDto :BookDto
{

    public IEnumerable<BookCoverDto> BookCovers {get;set;} = 
        new List<BookCoverDto>();

    public BookWithCoverDto(Guid id
        ,string Title
        ,string? description):base(id, Title,description)
    {
        
    }
}
