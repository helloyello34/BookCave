using System;
using System.Collections.Generic;
using System.Linq;
using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
  public class BookService
    {
        private BookRepo _bookRepo;

        public BookService()
        {
            //creates repository layer
            _bookRepo = new BookRepo();
        }

        public BookDetailsViewModel GetBookById(int id)
        {
            //gets book that matches id
            var book = _bookRepo.GetBookById(id);
            //gets comment for the book
            var bookComments = _bookRepo.GetComments(id);
            //if the book has comments add them else just return directly
            if( bookComments.Any() ) { book.Comments = bookComments; }

            return book;
        }
        public List<BookTableViewModel> GetBooksByGenre(int order)
        {
            //gets books of all genres and returns them
            var books = _bookRepo.GetBooksByGenre(order);
            return books;
        }
        public List<BookTableViewModel> GetBooksByGenre(string selectedGenre, int order)
        {
            //gets books of a specific genre and returns them
            var books = _bookRepo.GetBooksByGenre(selectedGenre, order);
            return books;
        }
        public BookFrontPageViewModel GetFrontPageBooks()
        {
            //gets front page books and returns them
            var books = _bookRepo.GetFrontPageBooks();
            return books;
        }
        public BookTopTenViewModel GetTopTenBooks()
        {
            //gets 10 top rated books and returns them
            var books = _bookRepo.GetTopTenBooks();

            return books;
        }
        public void AddBook(BookInputModel bookInputModel)
        {
            //returns new book
            var book = new Book
            {
                ISBN = bookInputModel.ISBN,
                Language = bookInputModel.Language,
                Image = bookInputModel.Image,
                Title = bookInputModel.Title,
                Genre = bookInputModel.Genre,
                Info = bookInputModel.Info,
                AuthorId = (int)bookInputModel.AuthorId,
                Publisher = bookInputModel.Publisher,
                PageCount = bookInputModel.PageCount,
                ReleaseYear = (int)bookInputModel.ReleaseYear,
                Price = (double)bookInputModel.Price,
                Discount = 0,
                Rating = 0,
                RatingCount = 0,
                Stock = 10
            };
            _bookRepo.AddBook(book);
        }

        public void AddAuthor(Author author)
        {
            //adds new author
            _bookRepo.AddAuthor(author);
        }
        
        public List<AuthorsViewModel> GetAuthorList()
        {
            //get author list from database
            return _bookRepo.GetAuthorList();
        }

        public void AddComment (int id, CommentInputModel comment)
        {
            //adds data into new comment
            var newComment = new Comment {
                BookId = id,
                Comments = comment.Comment,
                Rating = comment.Rating
            };
            
            _bookRepo.AddComment(newComment);
        }
        public void UpdateBookRating(int id, int rating)
        {
            //updates the overall rating of the book with the new rating
            var book = _bookRepo.GetBookEntity(id);
            var currentRatingSum = book.Rating * book.RatingCount;
            book.RatingCount++;
            book.Rating = (currentRatingSum + rating) / book.RatingCount;
            _bookRepo.UpdateBook(book);
        }
        public List<string> GetGenresList()
        {
            //gets a list of genres
            var genresList = _bookRepo.GetGenresList();

            return genresList;
        }

        public List<BookTableViewModel> findBooks(string searchString, int order)
        {
            //gets a list of books that fit the search strin
            var selectedBooks = _bookRepo.findBooks(searchString, order);

            return selectedBooks;
        }
        public BookEditInputModel GetBookEditInputModelById(int id)
        {
            var book = _bookRepo.GetBookEditInputModelById(id);
            return book;
        }
        public void EditBook(BookEditInputModel bookEditInputModel)
        {
            var book = _bookRepo.GetBookEntity(bookEditInputModel.Id);
            
            book.ISBN = bookEditInputModel.ISBN;
            book.Language = bookEditInputModel.Language;
            book.Image = bookEditInputModel.Image;
            book.Title = bookEditInputModel.Title;
            book.Genre = bookEditInputModel.Genre;
            book.Info = bookEditInputModel.Info;
            book.AuthorId = (int)bookEditInputModel.AuthorId;
            book.Publisher = bookEditInputModel.Publisher;
            book.PageCount = bookEditInputModel.PageCount;
            book.ReleaseYear = (int)bookEditInputModel.ReleaseYear;
            book.Price = (double)bookEditInputModel.Price;
            book.Rating = bookEditInputModel.Rating;
            book.RatingCount = bookEditInputModel.RatingCount;
            book.Stock = bookEditInputModel.Stock;
            
            _bookRepo.UpdateBook(book);            
        }
    }
}