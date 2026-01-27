using System.ComponentModel.DataAnnotations;

public class UpdateBookDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    public int AuthorId { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string CoverImageUrl { get; set; }
    public decimal AverageRating { get; set; }
}
