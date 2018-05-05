using System.Collections.Generic;
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
            return book;
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
                Genre = bookInputModel.Genre,
                Info = bookInputModel.Info,
                AuthorId = bookInputModel.AuthorId,
                Publisher = bookInputModel.Publisher,
                PageCount = bookInputModel.PageCount,
                ReleaseYear = bookInputModel.ReleaseYear,
                Price = bookInputModel.Price,
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
        /*public int GetNewBookId()
        {
            var id = _bookRepo.GetNewBookId();
            return id;
        }*/
    }
}