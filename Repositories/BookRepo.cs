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
                    //Genre = b.Genre,
                    Language = b.Language,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN
                }).SingleOrDefault();

            return book;
        }
        public BookListViewModel GetBooksByGenre()
        {
            var temp = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                select new BookDetailsViewModel
                {
                    Title = b.Title,
                    Author = a.Name,
                    ReleaseYear = b.ReleaseYear,
                    Rating = b.Rating,
                    Info = b.Info,
                    Price = b.Price,
                    Image = b.Image,
                    //Genre = b.GenreId,
                    Language = b.Language,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN
                }
            ).ToList();

            var books = new BookListViewModel
            {
                Books = temp
            };

            return books;
        }
        public BookListViewModel GetBooksByGenre(int[] selectedGenre)
        {     
            var temp = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                //where !selectedGenre.Except(b.Genre).Any()
                select new BookDetailsViewModel
                {
                    Title = b.Title,
                    Author = a.Name,
                    ReleaseYear = b.ReleaseYear,
                    Rating = b.Rating,
                    Info = b.Info,
                    Price = b.Price,
                    Image = b.Image,
                    //Genre = b.Genre,
                    Language = b.Language,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN
                }
            ).ToList();

            if(selectedGenre == null)
            {
                temp = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    select new BookDetailsViewModel
                    {
                        Title = b.Title,
                        Author = a.Name,
                        ReleaseYear = b.ReleaseYear,
                        Rating = b.Rating,
                        Info = b.Info,
                        Price = b.Price,
                        Image = b.Image,
                        //Genre = b.GenreId,
                        Language = b.Language,
                        Publisher = b.Publisher,
                        ISBN = b.ISBN
                    }
                ).ToList();
            }            

            var books = new BookListViewModel
            {
               // Books = temp
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
        }

        public void AddComment(Comment comment)
        {
            _db.Add(comment);
            _db.SaveChanges();
        }
        public Book GetBookEntity(int id)
        {
            var book = (
                from b in _db.Books
                where b.Id == id
                select b).SingleOrDefault();

                return book;            
        }
        public void UpdateBook(Book book)
        {
            _db.Update(book);
            _db.SaveChanges();
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

        public List<CommentViewModel> GetComments(int id)
        {
            var commentList = (
                from c in _db.Comments
                where c.BookId == id
                select new CommentViewModel {
                    Comment = c.Comments,
                    Rating = c.Rating
                }
            ).ToList();
            return commentList;
        }
    }
}