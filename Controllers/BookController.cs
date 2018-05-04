using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {

        private BookService _bookService;

        public BookController()
        {
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var book = _bookService.GetBookById(id);

            return View(book);
        }
        public IActionResult Top10()
        {
            var books = _bookService.GetTopTenBooks();
            return View(books);
        }
    }
}