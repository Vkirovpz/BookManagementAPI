using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Data
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books => Set<Book>();


    }
}
