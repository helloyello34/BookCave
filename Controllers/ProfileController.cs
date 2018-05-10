using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.InputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using BookCave.Data;
using BookCave.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using BookCave.Services;

namespace BookCave.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private BookService _bookService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bookService = new BookService();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public List<string> GetGenres()
        {
            var genres = _bookService.GetGenresList();
            return genres;            
        } 
        public async Task<IActionResult> Home()
        {
            var user = await GetCurrentUserAsync();

            var profile = new ProfileHomeViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetAddress = user.Street,
                ZipCode = user.ZipCode,
                City = user.City,
                Country = user.Country,
                ImageUrl = user.ImageUrl,
                FavoriteBook = user.FavoriteBook
            };
            ViewData["Genres"] = GetGenres();
            return View(profile);
        }

        public async Task<IActionResult> EditPersonal()
        {
            var user = await GetCurrentUserAsync();
            var person = new UserPersonalInputModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Birthday = user.Birthday,
                FavoriteBook = user.FavoriteBook
            };

            var image = user.ImageUrl;
            ViewData["Image"] = image;
            ViewData["Genres"] = GetGenres();

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonal(UserPersonalInputModel model)
        {
            if( ModelState.IsValid )
            {
                var user = await GetCurrentUserAsync();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Gender = model.Gender;
                user.ImageUrl = model.ImageUrl;
                user.Birthday = model.Birthday;
                user.FavoriteBook = model.FavoriteBook;

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Home", "Profile");
            }
            return View();
        }

        public async Task<IActionResult> EditShipping()
        {
            var user = await GetCurrentUserAsync();
            var person = new UserShippingInputModel {
                StreetAddress = user.Street,
                ZipCode = user.ZipCode,
                City = user.City,
                Country = user.Country
            };
            var image = user.ImageUrl;
            ViewData["Image"] = image; 
            ViewData["Genres"] = GetGenres();

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> EditShipping( UserShippingInputModel model )
        {

            if( ModelState.IsValid )
            {
                var user = await GetCurrentUserAsync();
                user.Street = model.StreetAddress;
                user.ZipCode = model.ZipCode;
                user.City = model.City;
                user.Country = model.Country;

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Home", "Profile");
            }

            return View();
        }
    }
}