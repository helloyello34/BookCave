using System.Collections.Generic;
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
        public IActionResult Top10()
        {
            var books = _bookService.GetTopTenBooks();
            return View(books);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add book to database";

            var authorList = _bookService.GetAuthorList();
            ViewData["aList"] = authorList as List<BookCave.Models.ViewModels.AuthorsViewModel>;

            return View();
        }

        [HttpPost]
        public IActionResult Create(BookInputModel bookInputModel)
        {
            if(ModelState.IsValid)
            {
                _bookService.AddBook(bookInputModel);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]

        public IActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAuthor(AuthorInputModel AuthorInputModel)
        {
            var author = new Author
            {
                Name = AuthorInputModel.Name
            };
            if(ModelState.IsValid)
            {
                _bookService.AddAuthor(author);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}