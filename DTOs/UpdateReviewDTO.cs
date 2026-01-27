using System.ComponentModel.DataAnnotations;

public class UpdateReviewDto
{
    [Required]
    public int ReviewId { get; set; }

    [Required]
    [Range(1, 5)]
    public decimal Rating { get; set; }

    [MaxLength(2000)]
    public string Review { get; set; }
}
