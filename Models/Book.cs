﻿using System.ComponentModel.DataAnnotations;
using static BookManagementAPI.Models.DataConstants.Book;

namespace BookManagementAPI.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maxTitle, MinimumLength = minTitle)]
        public string Title { get; set; }

        [Required]
        [StringLength(maxAuthor, MinimumLength = minAuthor)]
        public string Author { get; set; }

        [Required]
        [Range(minYear, maxYear)]
        public int PublicationYear { get; set; }

        public int BookViews { get; set; } = 0;
    }
}
