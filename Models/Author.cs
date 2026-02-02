using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Author
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AuthorID { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Biography { get; set; }
    public string ProfilePictureUrl { get; set; }

    public string? IdentityUserId { get; set; }

    public ICollection<Book> Books { get; set; }
    public ICollection<BookListAuthor> BookListAuthors { get; set; }
}
