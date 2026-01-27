using System.ComponentModel.DataAnnotations;

public class CreateBookListAuthorDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int BookListAuthorId { get; set; }
}
