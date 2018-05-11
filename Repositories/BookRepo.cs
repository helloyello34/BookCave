using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;
        public BookRepo()
        {
            //creates connection with database
            _db = new DataContext();
        }
        
        public BookDetailsViewModel GetBookById(int id)
        {
            //finds book from database that matches id and returns it
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
        public List<BookTableViewModel> GetBooksByGenre(int order)
        {
            //finds books of all genres and returns them as a list
            //if order is 0 returns books ordered by name
            var books = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                orderby b.Title
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image
                }
            ).ToList();

            //if oreder is 1 order books by rating
            if(order == 1)
            {
                books = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    orderby b.Rating
                    select new BookTableViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = a.Name,
                        Rating = b.Rating,
                        Price = b.Price,
                        Image = b.Image
                    }
                ).ToList();
            }
            //if order is 2 order books by price
            else if(order == 2)
            {
                books = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    orderby b.Price descending
                    select new BookTableViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = a.Name,
                        Rating = b.Rating,
                        Price = b.Price,
                        Image = b.Image
                    }
                ).ToList();
            }
            else if(order == 3)
            {
                books = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    orderby b.Price ascending
                    select new BookTableViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = a.Name,
                        Rating = b.Rating,
                        Price = b.Price,
                        Image = b.Image
                    }
                ).ToList();
            }

            return books;
        }
        public List<BookTableViewModel> GetBooksByGenre(string selectedGenre, int order)
        {
            //finds books that include all the genres in selected genres
            //if order is 0 returns books ordered by name
            var books = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                where !selectedGenre.ToLower().Except(b.Genre.ToLower()).Any()
                orderby b.Title
                select new BookTableViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = a.Name,
                    Rating = b.Rating,
                    Price = b.Price,
                    Image = b.Image
                }
            ).ToList();
            //if oreder is 1 order books by rating
            if(order == 1)
            {
                books = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    where !selectedGenre.ToLower().Except(b.Genre.ToLower()).Any()
                    orderby b.Rating descending
                    select new BookTableViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = a.Name,
                        Rating = b.Rating,
                        Price = b.Price,
                        Image = b.Image
                    }
                ).ToList();
            }
            //if order is 2 order books by price
            else if(order == 2)
            {
                books = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    where !selectedGenre.ToLower().Except(b.Genre.ToLower()).Any()
                    orderby b.Price
                    select new BookTableViewModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Author = a.Name,
                        Rating = b.Rating,
                        Price = b.Price,
                        Image = b.Image
                    }
                ).ToList();
            }

            return books;
        }
        public BookFrontPageViewModel GetFrontPageBooks()
        {
            //fetches books for the front page
            //Gets top 6 books by rating
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
                }
            ).Take(6).ToList();
            //gets the 6 most recently added books
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
                }
            ).Take(6).ToList();
        
            //gets 6 random books
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
                }
            ).Take(6).ToList();
            //combines queries into one model and returns it
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
            //gets top 10 books by rating
            //gets the first 5 books 
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
                }
            ).Take(5).ToList();
            //gets the last 5 books
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
                }
            ).Skip(5).Take(5).ToList();
            //combines queries into one model
            var books = new BookTopTenViewModel
            {
                FirstHalf = firstHalf,
                SecondHalf = secondHalf
            };

            return books;
        }

        public void AddBook(Book book)
        {
            //add book to database
            _db.Add(book);
            _db.SaveChanges();
        }

        public void AddAuthor(Author author)
        {
            //add auuthor to database
            _db.Add(author);
            _db.SaveChanges();
        }

        public void AddComment(Comment comment)
        {
            //adds comment to database
            _db.Add(comment);
            _db.SaveChanges();
        }
        public Book GetBookEntity(int id)
        {
            //gets book by id
            var book = (
                from b in _db.Books
                where b.Id == id
                select b
            ).SingleOrDefault();

                return book;            
        }
        public void UpdateBook(Book book)
        {
            //updates a book from database
            _db.Update(book);
            _db.SaveChanges();
        }
        public List<AuthorsViewModel> GetAuthorList()
        {
            //get list of authors from database
            var authorList = (
                from a in _db.Authors
                select new AuthorsViewModel {
                    Id = a.Id,
                    Name = a.Name
                    }
                ).ToList();
                return authorList;
        }

        public List<CommentViewModel> GetComments(int id)
        {
            //gets comments from database
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

        public IEnumerable<Book> Books => _db.Books.ToList();
        public Book GetBookOnId(int id) => _db.Books.FirstOrDefault(p => p.Id == id);
        public DbSet<Book> GetBooks()
        {
            //gets books from database
            var books = _db.Books;
            return books;
        }

        public DbSet<Author> GetAuthors()
        {
            //gets authors from database
            var authors = _db.Authors;
            return authors;
        }
        public List<string> GetGenresList()
        {
            //gets genres from database 
            var genres = (
                from g in _db.Genres
                select g
            ).ToList();
                List<string> genreList = new List<string>();
                foreach(var str in genres)
                {
                    genreList.Add(str.Name);
                }
            return genreList;
        }
        public List<BookTableViewModel> findBooks(string searchString, int order)
        {
            //find books that have the same title or author as the search string
            //if order is 0 order them by title
            var selectedBooks = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                where b.Title.ToLower().Contains(searchString.ToLower()) ||  a.Name.ToLower().Contains(searchString.ToLower())
                orderby b.Title
                select new BookTableViewModel {
                    Id = b.Id,
                    Title = b.Title,
                    Rating = b.Rating,
                    Author = a.Name,
                    Price = b.Price,
                    Image = b.Image,
                    Discount = b.Discount
                }
            ).ToList();
            //if order is 1 order books by rating
            if(order == 1)
            {
                selectedBooks = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    where b.Title.ToLower().Contains(searchString.ToLower()) ||  a.Name.ToLower().Contains(searchString.ToLower())
                    orderby b.Rating descending
                    select new BookTableViewModel {
                        Id = b.Id,
                        Title = b.Title,
                        Rating = b.Rating,
                        Author = a.Name,
                        Price = b.Price,
                        Image = b.Image,
                        Discount = b.Discount
                    }
                ).ToList();
            }
            //if order is 2 order books by price
            else if(order == 2)
            {
                selectedBooks = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    where b.Title.ToLower().Contains(searchString.ToLower()) ||  a.Name.ToLower().Contains(searchString.ToLower())
                    orderby b.Price ascending
                    select new BookTableViewModel {
                        Id = b.Id,
                        Title = b.Title,
                        Rating = b.Rating,
                        Author = a.Name,
                        Price = b.Price,
                        Image = b.Image,
                        Discount = b.Discount
                    }
                ).ToList();
            }
            else if(order == 3)
            {
                selectedBooks = (
                    from b in _db.Books
                    join a in _db.Authors on b.AuthorId equals a.Id
                    where b.Title.ToLower().Contains(searchString.ToLower()) ||  a.Name.ToLower().Contains(searchString.ToLower())
                    orderby b.Price descending
                    select new BookTableViewModel {
                        Id = b.Id,
                        Title = b.Title,
                        Rating = b.Rating,
                        Author = a.Name,
                        Price = b.Price,
                        Image = b.Image,
                        Discount = b.Discount
                    }
                ).ToList();
            }

            return selectedBooks;
        }
        public BookEditInputModel GetBookEditInputModelById(int id)
        {
            var book = (
                from b in _db.Books
                where b.Id == id
                select new BookEditInputModel
                {
                    Id = b.Id,
                    ISBN = b.ISBN,
                    Language = b.Language,
                    Image = b.Image,
                    Title = b.Title,
                    Genre = b.Genre,
                    Info = b.Info,
                    AuthorId = b.AuthorId,
                    Publisher = b.Publisher,
                    PageCount = b.PageCount,
                    ReleaseYear = b.ReleaseYear,
                    Price = b.Price,
                    Rating = b.Rating,
                    RatingCount = b.RatingCount,
                    Stock = b.Stock
                }
            ).SingleOrDefault();

                return book;
        }
        public void DeleteBook(int id)
        {
            var book = (
                from b in _db.Books
                where b.Id == id
                select b).SingleOrDefault();

            _db.Remove(book);
            _db.SaveChanges();
        }
        public void AddGenre(Genre genre)
        {
            _db.Add(genre);
            _db.SaveChanges();
        }
    }
}