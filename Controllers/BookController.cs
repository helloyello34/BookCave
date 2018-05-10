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
            //creates service layer
            _bookService = new BookService();
        }
        public List<string> GetGenres()
        {
            //get a list of genres from database
            var genres = _bookService.GetGenresList();
            return genres;            
        }   
        public IActionResult ListBooks(string selectedGenre, int order)
        {
            ViewData["Genres"] = GetGenres();
            ViewData["currentGenre"] = selectedGenre;
            //if no genre is selected. fetch all books
            var books = _bookService.GetBooksByGenre(order);
            //If one ore more genres are selected. fetch books that fall nuder both genres
            if(selectedGenre != null)
            {
               books = _bookService.GetBooksByGenre(selectedGenre, order);
            }
            return View(books);
        }
        public IActionResult Details(int id)
        {
            ViewData["Genres"] = GetGenres();
            //Finds a specific book from the database
            var book = _bookService.GetBookById(id);
            //if a book is found display it
            if( book != null ) { return View(book); }
            //if no book is found redirect to homepage
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Details(int id, CommentInputModel model)
        {
            //adds a rating and comment to book
            //If you rate the book without writing a comment only the rating is added
            if(model.Comment != null)
            {
            _bookService.AddComment(id, model);
            }
            //updates ranking
            _bookService.UpdateBookRating(id, Convert.ToInt32(model.Rating));
            return RedirectToAction("Details", id);
        }

        public IActionResult Top10()
        {
            //shows top 10 books by rating
            ViewData["Genres"] = GetGenres();
            //fetches top 10 books from database
            var books = _bookService.GetTopTenBooks();
            return View(books);
        }
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            //default view for creating a new book
            ViewData["Genres"] = GetGenres();
            ViewData["Title"] = "Add book to database";

            var authorList = _bookService.GetAuthorList();
            ViewData["aList"] = authorList as List<BookCave.Models.ViewModels.AuthorsViewModel>;

            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Create(BookInputModel bookInputModel)
        {
            //create a book
            ViewData["Title"] = "Add book to database";
            ViewData["Genres"] = GetGenres();
            //fetches list of authors
            var authorList = _bookService.GetAuthorList();
            ViewData["aList"] = authorList as List<BookCave.Models.ViewModels.AuthorsViewModel>;
            //if input model is valid add book to database and redirect to the new book's details page
            if(ModelState.IsValid)
            {
                _bookService.AddBook(bookInputModel);
                
                return RedirectToAction("Details", bookInputModel.Id);
            }
            //if model is invalid refresh page
            return View(bookInputModel);
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult CreateAuthor()
        {
            //default page for creating an author
            ViewData["Genres"] = GetGenres();
            return View();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult CreateAuthor(AuthorInputModel AuthorInputModel)
        {
            //creates new author
            var author = new Author
            {
                Name = AuthorInputModel.Name
            };
            //if author is valid add him to the database and redirect to create book page
            if(ModelState.IsValid)
            {
                _bookService.AddAuthor(author);
                return RedirectToAction("Create");
            }
            //if model is invalid refresh page
            return View();
        }
        [HttpGet]
        [Authorize(Roles="Admin")]
        public IActionResult CreateGenre()
        {
            ViewData["Genres"] = GetGenres();
            return View();
        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult CreateGenre(GenreInputModel genreInputModel)
        {
            ViewData["Genres"] = GetGenres();
            var genre = new Genre
            {
                Name = genreInputModel.Genre
            };
            if(ModelState.IsValid)
            {
                _bookService.AddGenre(genre);
                return RedirectToAction("Home/Index");
            }
            return View();
        }

        public IActionResult BookNotFound()
        {
            //error page for if book isn't found
            ViewData["Genres"] = GetGenres();
            return View();
        }

        [HttpGet]
        public IActionResult Search(string q, int order)
        {
            //search for books, titles, authors etc.
            ViewData["currentQuery"] = q;
            ViewData["Genres"] = GetGenres();
            //if search string is not empty search for books and return them into model
            if (q != null)
            {
                ViewData["SearchString"] = q;
                var searchedBooks = _bookService.findBooks(q, order);
                //if list comes back empty redirect to error page
                if(searchedBooks.Count != 0)
                {
                    return View(searchedBooks);
                }
            }
            return RedirectToAction("BookNotFound");
        }
        [Authorize(Roles="Admin")]
        public IActionResult EditBook(int id)
        {
            var authorList = _bookService.GetAuthorList();
            ViewData["aList"] = authorList as List<BookCave.Models.ViewModels.AuthorsViewModel>;
            ViewData["Genres"] = GetGenres();
            var book = _bookService.GetBookEditInputModelById(id);

            return View(book);

        }
        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult EditBook(BookEditInputModel bookEditInputModel)
        {
            ViewData["Genres"] = GetGenres();

            if(ModelState.IsValid)
            {
                _bookService.EditBook(bookEditInputModel);
                return RedirectToAction("Home/Index");
            }
            return View();
        }
        [Authorize(Roles="Admin")]
        public IActionResult DeleteBook(int id)
        {
            ViewData["Genres"] = GetGenres();
            _bookService.DeleteBook(id);

            return RedirectToAction("Home/Index");
        }
    }
}