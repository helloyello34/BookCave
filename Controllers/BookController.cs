using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
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
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookInputModel BookInputModel)
        {
            var book = new Book
            {
                ISBN = BookInputModel.ISBN,
                Language = BookInputModel.Language,
                Image = BookInputModel.Image,
                Title = BookInputModel.Title,
                Genre = BookInputModel.Genre,
                Info = BookInputModel.Info,
                AuthorId = BookInputModel.AuthorId,
                Publisher = BookInputModel.Publisher,
                PageCount = BookInputModel.PageCount,
                ReleaseYear = BookInputModel.ReleaseYear,
                Price = BookInputModel.Price,
                Discount = 0,
                Rating = 0,
                RatingCount = 0,
                Stock = 10
            };
            if(ModelState.IsValid)
            {
                _bookService.AddBook(book);
                RedirectToAction("Index");
            }
            return View();
        }
    }
}