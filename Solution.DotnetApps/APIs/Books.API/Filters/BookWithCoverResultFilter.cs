using AutoMapper;
using Books.API.Entities;
using Books.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters;

public class BookWithCoverResultFilter : IAsyncResultFilter
{
    private readonly IMapper _mapper;

    public BookWithCoverResultFilter(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var result = context.Result as ObjectResult;
        if (result?.Value == null 
            || result.StatusCode < 200 || result.StatusCode >=  300)
        {
            await next();
            return;
        }

        var (book , bookCovers) = ((Book ,List<BookCover?>))result.Value;
        var bookwithCovers =_mapper.Map<BookWithCoverDto>(book);
        result.Value =  _mapper.Map(bookCovers, bookwithCovers);

        await next();
        
    }
}
