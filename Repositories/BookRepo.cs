using BookCave.Data;
using BookCave.Models.ViewModels;
using System.Linq;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;
        public BookRepo()
        {
            _db = new DataContext();
        }
        
        public BookDetailsViewModel GetBookById(int id)
        {
            var book = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                where b.Id == id
                select new BookDetailsViewModel {

                    Title = b.Title,
                    Author = a.Name,
                    ReleaseYear = b.ReleaseYear,
                    Rating = b.Rating,
                    Info = b.Info,
                    Price = b.price,
                    Image = b.Image,
                    Genre = b.Genre,
                    Language = b.Language,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN
                }).SingleOrDefault();

            return book;
        }
    }
}