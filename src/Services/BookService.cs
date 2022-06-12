namespace Api.Services;

public class BookService
{
    private readonly IMongoCollection<Book> c_books;

    public BookService(IBookstoreDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        c_books = database.GetCollection<Book>(settings.BooksCollectionName);
    }


    public async Task<List<Book>> Get()
    {
        var _booksCollection = await c_books.FindAsync<Book>(book => true);
        return _booksCollection.ToList();
    }


    public async Task<Book> Get(string id)
    {
        var _book = await c_books.FindAsync<Book>(book => book.Id == id);
        return _book.FirstOrDefault();
    }


    public async Task<Book> Create(Book book)
    {
        await c_books.InsertOneAsync(book);
        return book;
    }


    public async Task Update(string id, Book bookIn)
    {
        await c_books.ReplaceOneAsync(book => book.Id == id, bookIn);
    }


    public async Task Remove(Book bookIn)
    {
        await c_books.DeleteOneAsync(book => book.Id == bookIn.Id);
    }


    public async Task Remove(string id)
    {
        await c_books.DeleteOneAsync(book => book.Id == id);
    }
}
