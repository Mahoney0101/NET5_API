namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookService c_bookService;

    public BooksController(BookService bookService)
    {
        c_bookService = bookService;
    }


    [HttpGet]
    [Route("Time")]
    public string GetTime()
    {
        return DateTime.Now.ToString("h:mm:ss tt");
    }


    [HttpGet]
    public async Task<ActionResult<List<Book>>> Get() 
    { 
        return await c_bookService.Get();
    }


    [HttpGet("{id}", Name = "GetBook")]
    public async Task<ActionResult<Book>> Get(string id)
    {
        var book = await c_bookService.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }


    [HttpPost]
    public async Task<ActionResult<Book>> Create(Book book)
    {
        await c_bookService.Create(book);
        ArgumentNullException.ThrowIfNull(book.Id);

        return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Book bookIn)
    {
        var book = await c_bookService.Get(id);

        if (book == null)
        {
            return NotFound();
        }

        await c_bookService.Update(id, bookIn);

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await c_bookService.Get(id);

        if (book == null)
        {
            return NotFound();
        }
        ArgumentNullException.ThrowIfNull(book.Id);
        await c_bookService.Remove(book.Id);

        return NoContent();
    }
}
