namespace Books.API.Entities;

public class BookCover
{
    public string Id { get; set; }
    public byte[]? Content { get; set; }

    public BookCover(string id, byte[]? content)
    {
        Id = id;
        Content = content;
    }
}