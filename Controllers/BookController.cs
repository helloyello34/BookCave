using System;
using System.Collections.Generic;
using BookCave.Models.EntityModels;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult ListBooks(string selectedGenre, int order)
        {
            var books = _bookService.GetBooksByGenre(order);
            if(selectedGenre != null)
            {
               books = _bookService.GetBooksByGenre(selectedGenre, order);
            }
            return View(books);
        }
        public IActionResult Details(int id)
        {
            var book = _bookService.GetBookById(id);

            if( book != null ) { return View(book); }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Details(int id, CommentInputModel model)
        {
            if(model.Comment != null)
            {
            _bookService.AddComment(id, model);
            }
            //var book = _bookService.GetBookById(id); //onotuð lina ?
            _bookService.UpdateBookRating(id, Convert.ToInt32(model.Rating));
            return RedirectToAction("Details", id);
        }

        public IActionResult Top10()
        {
            var genres = _bookService.GetGenresList();
            
            ViewData["Genres"] = genres;
            var books = _bookService.GetTopTenBooks();
            return View(books);
        }
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add book to database";

            var authorList = _bookService.GetAuthorList();
            ViewData["aList"] = authorList as List<BookCave.Models.ViewModels.AuthorsViewModel>;

            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Create(BookInputModel bookInputModel)
        {
             ViewData["Title"] = "Add book to database";

            var authorList = _bookService.GetAuthorList();
            ViewData["aList"] = authorList as List<BookCave.Models.ViewModels.AuthorsViewModel>;
            if(ModelState.IsValid)
            {
                _bookService.AddBook(bookInputModel);
                return RedirectToAction("Index");
            }
            return View(bookInputModel);
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

        public IActionResult BookNotFound()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string q)
        {
            if (q != null)
            {
                ViewData["SearchString"] = q;
                var searchedBooks = _bookService.findBooks(q);
                if(searchedBooks.Count != 0)
                {
                    return View(searchedBooks);
                }
            }
            return RedirectToAction("BookNotFound");
        }
    }
}