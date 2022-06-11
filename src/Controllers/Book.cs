namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookService _bookService;

    public BooksController(BookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    [Route("Time")]
    public string GetTime()
    {
        return DateTime.Now.ToString("h:mm:ss tt");
    }

    [HttpGet]
    public ActionResult<List<Book>> Get() =>
        _bookService.Get();

    [HttpGet("{id}", Name = "GetBook")]
    public ActionResult<Book> Get(string id)
    {
        var book = _bookService.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public ActionResult<Book> Create(Book book)
    {
        _bookService.Create(book);
        ArgumentNullException.ThrowIfNull(book.Id);
        return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, Book bookIn)
    {
        var book = _bookService.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        _bookService.Update(id, bookIn);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var book = _bookService.Get(id);

        if (book == null)
        {
            return NotFound();
        }
        ArgumentNullException.ThrowIfNull(book.Id);
        _bookService.Remove(book.Id);

        return NoContent();
    }
}
