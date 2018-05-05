using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using System;
using System.Collections.Generic;
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
                    Price = b.Price,
                    Image = b.Image,
                    Genre = b.Genre,
                    Language = b.Language,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN
                }).SingleOrDefault();

            return book;
        }
        public BookListViewModel GetBooksByGenre(string selectedGenre)
        {
            var temp = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                where b.Genre.ToLower().Contains(selectedGenre.ToLower())
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }
            ).ToList();

            if(selectedGenre == null)
            {
                temp = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    select new BookTableViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = a.Name,
                        Rating = b.Rating,
                        Price = b.Price,
                        Image = b.Image,
                        Discount = b.Discount
                    }
                ).ToList();
            }            

            var books = new BookListViewModel
            {
                Books = temp
            };

            return books;
        }
        public BookFrontPageViewModel GetFrontPageBooks()
        {
            var popularBooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Rating descending
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(6).ToList();

            var recentlyAddedbooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Id descending
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(6).ToList();

            /*var genre = (
                from b in _db.Books
                select b.Genre).ToList();

            var random = new Random();
            var index = random.Next(0, genre.Count() + 1);
            var chosenGenre = genre[index]; */             

            var randomBooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby Guid.NewGuid()
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(6).ToList();

            var books = new BookFrontPageViewModel
            {
                PopularBooks = popularBooks,
                RecentlyAddedBooks = recentlyAddedbooks,
                RandomBooks = randomBooks
            };
            return books;
        }
        public BookTopTenViewModel GetTopTenBooks()
        {
            var firstHalf = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Rating descending
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Take(5).ToList();
            var secondHalf = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Rating descending
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }).Skip(5).Take(5).ToList();
            var books = new BookTopTenViewModel
            {
                FirstHalf = firstHalf,
                SecondHalf = secondHalf
            };
                return books;
        }

        public void AddBook(Book book)
        {
            _db.Add(book);
            _db.SaveChanges();
        }

        public void AddAuthor(Author author)
        {
            _db.Add(author);
            _db.SaveChanges();
            Console.Write("Book added");
        }

        public List<AuthorsViewModel> GetAuthorList()
        {
            var authorList = (
                from a in _db.Authors
                select new AuthorsViewModel {
                    Id = a.Id,
                    Name = a.Name
                    }).ToList();
                return authorList;
        }
      /*  public int GetNewBookId()
        {
            var id = 0;
                foreach (var m in _db.Books)
                {
                    if(id < m.Id)   
                    {
                        id = m.Id;
                    };
                }
                return id + 1;
        }*/
    }
}