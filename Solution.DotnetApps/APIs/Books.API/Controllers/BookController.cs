using AutoMapper;
using Books.API.DataAccess;
using Books.API.Entities;
using Books.API.Filters;
using Books.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository
            , IMapper mapper)
        {
            _bookRepository = bookRepository as BookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper;
        }

        [HttpGet("books")]
        [TypeFilter(typeof(BookResultFilter<IEnumerable<BookDto>>))]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            if(books == null )
                return NotFound();
            return Ok(books);
        }

        [HttpGet("{id:guid}",Name ="GetBookById")]
        [TypeFilter(typeof(BookResultFilter<BookDto>))]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null) return NotFound();
            return Ok(book);
        }

        [HttpGet("book/withcover/{guid:guid}", Name ="GetBookWithCovers")]
        [TypeFilter(typeof(BookWithCoverResultFilter))]
        public async Task<IActionResult> GetBookWithcovers(Guid guid,
            CancellationToken token)
        {
            var book = await _bookRepository.GetBookByIdAsync(guid);
            if(book == null) return NotFound();
            //var bookCover = await _bookRepository.GetBookCoverAsync(guid);
            //var bookCovers = await _bookRepository.GetBookCoversProcessAfterWaitForAllAsync(guid);
            var bookCovers = await _bookRepository.GetBookCoversProcessOneByOneAsync(guid, token);
            return Ok((book, bookCovers.ToList()));
        }

        [HttpPost("book")]
        [TypeFilter(typeof(BookResultFilter<BookDto>))]
        public async Task<IActionResult> AddBookDetails([FromBody] BookForCreationDto bookCreationDto)
        {
            var book = _mapper.Map<Book>(bookCreationDto);
            _bookRepository.AddBook(book);
            var isCreated = await _bookRepository.SaveChangesAsync();
            if (!isCreated)
                return BadRequest();
            book = await _bookRepository.GetBookByIdAsync(book.Id);
            return CreatedAtRoute("GetBookById", new { id = book.Id },
                book);

        }
    }
}
