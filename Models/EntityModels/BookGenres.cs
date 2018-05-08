namespace BookCave.Models.EntityModels
{
    public class BookGenres
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int GenreId { get; set; }
    }
}