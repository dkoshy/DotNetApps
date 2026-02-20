using System.ComponentModel.DataAnnotations;

namespace Books.API.Models;

public class BookForCreationDto
{

    public BookForCreationDto(string Title 
        , string? Description 
        , Guid AuthorId)
    {
        this.Title = Title;
        this.Description = Description;
        this.AuthorId = AuthorId;
    }
       
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public Guid AuthorId { get; set; }

}
