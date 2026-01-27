using System.ComponentModel.DataAnnotations;

public class CreateReviewDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public int ReaderId { get; set; }

    [Required]
    [Range(1, 5)]
    public decimal Rating { get; set; }

    [MaxLength(2000)]
    public string Review { get; set; }
}
