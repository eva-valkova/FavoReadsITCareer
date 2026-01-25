using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookID { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public Author Author { get; set; }
    public int AuthorID { get; set; }
    public ICollection<BookListReader> BookListReader { get; set; }
    public ICollection<BookListAuthor> BookListAuthor { get; set; }

}
