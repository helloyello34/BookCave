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
            //creates service layer
            _bookService = new BookService();
        }
        public List<string> GetGenres()
        {
            //fetches genres from database
            var genres = _bookService.GetGenresList();
            return genres;            
        }
        public IActionResult Index()
        { 
            //home page
            ViewData["Genres"] = GetGenres();
            //fetches books to be displayed on the home page
            var books = _bookService.GetFrontPageBooks();
            return View(books);
        }

        public IActionResult About()
        {
            //about page
            ViewData["Message"] = "Your application description page.";
            ViewData["Genres"] = GetGenres();
            return View();
        }

        public IActionResult Contact()
        {
            //contact us page
            ViewData["Message"] = "Your contact page.";
            ViewData["Genres"] = GetGenres();


            return View();
        }

        public IActionResult Error()
        {
            //error page
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
