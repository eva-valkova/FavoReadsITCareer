using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookListReader
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookListReaderID { get; set; }

    public int BookID { get; set; }
    public Book Book { get; set; }

    // FK → Reader
    public int ReaderID { get; set; }
    public Reader Reader { get; set; }

    public decimal BookRating { get; set; }
    public string BookReview {  get; set; }

}
