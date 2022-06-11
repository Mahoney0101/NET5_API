namespace Api.Models;

public class Book
{
    [BsonId]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string? BookName { get; set; }

    public string? Category { get; set; }

    public string? Author { get; set; }
}
