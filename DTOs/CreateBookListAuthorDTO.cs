using System.ComponentModel.DataAnnotations;

namespace FavoReads.DTOs
{
    public class CreateBookListAuthorDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int BookListAuthorId { get; set; }
    }
}