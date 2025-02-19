using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Data
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "RichDad PoorDad",
                    Author = "Robert Kyosaky",
                    PublicationYear = 1992,
                    BookViews = 0
                },
                new Book
                {
                    Id = 2,
                    Title = "Club 5 in the morening",
                    Author = "Unknown",
                    PublicationYear = 2000,
                    BookViews = 0
                },
                new Book
                {
                    Id = 3,
                    Title = "Harry Potter",
                    Author = "Some Magician",
                    PublicationYear = 2002,
                    BookViews = 0
                }
            );
        }
    }
}
