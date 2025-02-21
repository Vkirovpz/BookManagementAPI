namespace BookManagementAPI.Models
{
    public class BookDto
    {
        private const double const1 = 0.5;
        private const int const2 = 2;
        private int currentYear = DateTime.Now.Year;
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public int PublicationYear { get; set; }

        public int BookViews { get; set; }
        public int YearsSIncePublished => currentYear - this.PublicationYear;
        public double PopularityScore => this.BookViews * const1 + this.YearsSIncePublished * const2;
    }
}
