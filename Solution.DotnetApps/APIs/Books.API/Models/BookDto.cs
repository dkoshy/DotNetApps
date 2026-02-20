namespace Books.API.Models;

public class BookDto
{

    public BookDto(Guid Id, string Title , string? Description)
    {
        this.Id = Id;
        this.Title = Title;
        this.Description = Description;
        Author = string.Empty;
   }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string? Description { get; set; }

}
