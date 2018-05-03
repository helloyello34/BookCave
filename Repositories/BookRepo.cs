using BookCave.Data;
using BookCave.Models.ViewModels;
using System;
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
                select new BookDetailsViewModel 
                {
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
        public BookFrontPageViewModel GetFrontPageBooks()
        {
            var popularBooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Rating descending
                select new BookTableViewModel
                {
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(6).ToList();

            var recentlyAddedbooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Id descending
                select new BookTableViewModel
                {
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(6).ToList();
            var randomBooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby Guid.NewGuid()
                select new BookTableViewModel
                {
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(6).ToList();
                //list = list.OrderBy(emp => Guid.NewGuid()).ToList();

            var books = new BookFrontPageViewModel
            {
                PopularBooks = popularBooks,
                RecentlyAddedBooks = recentlyAddedbooks,
                RandomBooks = randomBooks
            };
            return books;
        }
    }
}