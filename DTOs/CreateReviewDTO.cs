using System.ComponentModel.DataAnnotations;
namespace FavoReads.DTOs
{
    public class CreateReviewDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int ReaderId { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [MaxLength(2000)]
        public string Review { get; set; }
    }
}