using AutoMapper;
using Books.API.Entities;
using Books.API.Models;

namespace Books.API.Profiles;

public class BookManagementProfile : Profile
{

    public BookManagementProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(b => b.Author, op => op.MapFrom(s => $"{s.Author.FirstName},{s.Author.LastName}"))
            .ConstructUsing(s => new BookDto(s.Id, s.Title, s.Description));

        CreateMap<BookForCreationDto, Book>()
            .ConstructUsing(src => new Book(Guid.NewGuid()
            , src.AuthorId
            , src.Title
            , src.Description));

        CreateMap<Book, BookWithCoverDto>()
            .ForMember(b => b.Author
             , op => op.MapFrom(src => $"{src.Author.FirstName},{src.Author.LastName}"))
            .ConstructUsing(src => new BookWithCoverDto(src.Id
            , src.Title
            , src.Description));

        CreateMap<BookCover, BookCoverDto>();
        CreateMap<IEnumerable<BookCover>, BookWithCoverDto>()
            .ForMember(dest => dest.BookCovers, opt => opt.MapFrom(src => src));
    }
}
