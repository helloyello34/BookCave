using BookCave.Models.EntityModels;
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

        public void AddBook(Book book)
        {
            _bookRepo.AddBook(book);
        }
        
        /*public int GetNewBookId()
        {
            var id = _bookRepo.GetNewBookId();
            return id;
        }*/
    }
}