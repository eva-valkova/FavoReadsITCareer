using System.ComponentModel.DataAnnotations;
namespace FavoReads.DTOs
{
    public class UpdateReviewDto
    {
        [Required]
        public int ReviewId { get; set; }

        [Required]
        [Range(1, 5)]
        public double Rating { get; set; }

        [MaxLength(2000)]
        public string Review { get; set; }
    }
}