using System.ComponentModel.DataAnnotations;
using static BookManagementAPI.Models.DataConstants.Book;

namespace BookManagementAPI.Models
{
    public class UpdateBookDto
    {
        [Required]
        [StringLength(maxTitle, MinimumLength = minTitle)]
        public string Title { get; set; }

        [Required]
        [StringLength(maxAuthor, MinimumLength = minAuthor)]
        public string Author { get; set; }

        [Required]
        [Range(minYear, maxYear)]
        public int PublicationYear { get; set; }
    }
}
