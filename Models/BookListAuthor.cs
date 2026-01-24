using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookListAuthor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookListAuthorID { get; set; }


    // FK → Book
    public int BookID { get; set; }
    public Book Book { get; set; }

    // FK → Author
    public int AuthorID { get; set; }
    public Author Author { get; set; }

}
