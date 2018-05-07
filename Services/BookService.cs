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
            _bookRepo = new BookRepo();
        }

        public BookDetailsViewModel GetBookById(int id)
        {
            var book = _bookRepo.GetBookById(id);
            var bookComments = _bookRepo.GetComments(id);
            if( bookComments.Any() ) { book.Comments = bookComments; }

            return book;
        }
        public BookListViewModel GetBooksByGenre()
        {
            var books = _bookRepo.GetBooksByGenre();
            return books;
        }
        public BookListViewModel GetBooksByGenre(int[] selectedGenre)
        {
            var books = _bookRepo.GetBooksByGenre(selectedGenre);
            return books;
        }
        public BookFrontPageViewModel GetFrontPageBooks()
        {
            var books = _bookRepo.GetFrontPageBooks();
            return books;
        }
        public BookTopTenViewModel GetTopTenBooks()
        {
            var books = _bookRepo.GetTopTenBooks();

            return books;
        }
        public void AddBook(BookInputModel bookInputModel)
        {
            var book = new Book
            {
                ISBN = bookInputModel.ISBN,
                Language = bookInputModel.Language,
                Image = bookInputModel.Image,
                Title = bookInputModel.Title,
                // Genre = bookInputModel.Genre,
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
            _bookRepo.AddAuthor(author);
        }
        
        public List<AuthorsViewModel> GetAuthorList()
        {
            return _bookRepo.GetAuthorList();
        }

        public void AddComment (int id, CommentInputModel comment)
        {
            var newComment = new Comment {
                BookId = id,
                Comments = comment.Comment,
                Rating = comment.Rating
            };
            
            _bookRepo.AddComment(newComment);
        }
        public void UpdateBookRating(int id, int rating)
        {
            var book = _bookRepo.GetBookEntity(id);
            var currentRatingSum = book.Rating * book.RatingCount;
            book.RatingCount++;
            book.Rating = (currentRatingSum + rating) / book.RatingCount;
            _bookRepo.UpdateBook(book);
        }

    }
}