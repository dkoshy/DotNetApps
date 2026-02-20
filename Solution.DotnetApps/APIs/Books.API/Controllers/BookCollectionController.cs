using AutoMapper;
using Books.API.DataAccess;
using Books.API.Entities;
using Books.API.Filters;
using Books.API.Models;
using Books.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers;

[Route("api/bookcollection")]
[ApiController]
public class BookCollectionController : ControllerBase
{
    private readonly BookRepository _repository;
    private readonly IMapper _mapper;

    public BookCollectionController(IBookRepository repository
       , IMapper mapper)
    {
        _repository = repository as BookRepository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper;
    }

    [HttpGet("({ids})", Name = "GetCollectionByIds")]
    [TypeFilter(typeof(BookResultFilter<IEnumerable<BookDto>>))]
    public async Task<IActionResult> GetCollectionByIds(
        [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var bookCollection = await _repository.GetAllAsync(ids);
        if (bookCollection == null || !bookCollection.Any())
            return NotFound();
        return Ok(bookCollection);
    }

    [HttpGet(Name = "GetCollection")]
    [TypeFilter(typeof(BookResultFilter<IEnumerable<BookDto>>))]
    public async Task<IActionResult> GetCollection([FromQuery] IList<Guid> ids)
    {
        var bookCollection = await _repository.GetAllAsync(ids);
        return Ok(bookCollection);
    }


    [HttpPost]
    [TypeFilter(typeof(BookResultFilter<IEnumerable<BookDto>>))]
    public async Task<IActionResult> BulkInsert([FromBody] IEnumerable<BookForCreationDto> books)
    {
        var bookEntities = _mapper.Map<IEnumerable<Book>>(books);
        _repository.AddMultiple(bookEntities);
        var iscreated = await _repository.SaveChangesAsync();
        if (!iscreated)
            return BadRequest();
        //var bookids = bookEntities.ToList().Select(b => b.Id); //For Query string
        var bookids = string.Join(",", bookEntities.Select(b => b.Id));
        var booksCollection = await _repository.GetAllAsync(bookEntities.Select(b => b.Id));
        return CreatedAtRoute("GetCollectionByIds", new {ids = bookids }, booksCollection);
    }

    //streaming example
    [HttpGet("streambook")]
    public async IAsyncEnumerable<BookDto> GetStreamOfBooks()
    {
        var bookStream =  _repository.GetBookStream();
        await foreach(var book in bookStream)
        {
            //adding some delay for testing purpose.
           await Task.Delay(500);
           yield return _mapper.Map<BookDto>(book);
        }
    }

}
