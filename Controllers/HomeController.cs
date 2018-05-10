using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Services;
using BookCave.Models.ViewModels;

namespace BookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;

        public HomeController()
        {
            _bookService = new BookService();
        }
        public List<string> GetGenres()
        {
            var genres = _bookService.GetGenresList();
            return genres;            
        }
        public IActionResult Index()
        { 
            ViewData["Genres"] = GetGenres();

            var books = _bookService.GetFrontPageBooks();
            return View(books);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["Genres"] = GetGenres();
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            ViewData["Genres"] = GetGenres();


            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
