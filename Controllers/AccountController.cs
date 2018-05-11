using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BookCave.Services;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private BookService _bookService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _bookService = new BookService();
        }
        public List<string> GetGenres()
        {
            //gets a list of all genres from the database
            var genres = _bookService.GetGenresList();
            return genres;            
        }  
        public async Task CreateAdmin()
        {
            //create admin account
            var user = await _userManager.FindByNameAsync("admin@admin.is");

            if(user == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@admin.is",
                    Email = "admin@admin.is",
                    FirstName = "admin",
                    LastName = "admin",
                    Street = "",
                    ZipCode = "",
                    City = "",
                    Country = "",
                    Gender = "",
                    Phone = ""
                };
                string adminPassword = "Admin123!";
                var createAdminUser = await _userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    //Admin given role
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }         
        } 
       public async Task<IActionResult> Login()
        {
            ViewData["Genres"] = GetGenres();
            //Þetta fall er kallað í til að bua til admin user
            //utaf það virkaði ekki inn í startup.cs
            await CreateAdmin();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Login(LoginViewModel model)
        {
            //Log in user
            //if login fails refresh page
            ViewData["Genres"] = GetGenres();

            if (!ModelState.IsValid) { return View(); }
            //search database for info
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            //if account is found and info is valid log the user in and redirect to homepage
            if ( result.Succeeded )
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            //Default register page
            ViewData["Genres"] = GetGenres();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            ViewData["Genres"] = GetGenres();
            //If user input is invalid refresh the page
            if ( !ModelState.IsValid ) { return View(); }
            //create user with initial required info
            var user = new ApplicationUser 
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Street = "",
                ZipCode = "",
                City = "",
                Country = "",
                Gender = "",
                Phone = ""
            };
            //check if info is valid
            var result = await _userManager.CreateAsync(user, model.Password);
            //If info is vaild register user, else refresh page
            if ( result.Succeeded )
            {
                // The user is successfullly registered

                // Add the concatenated first andd last name as fullName in claims

                await _userManager.AddClaimAsync(user, new Claim("FullName", $"{model.FirstName} {model.LastName}"));
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");

            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            //log out current user and redirect to login screen
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult AccessDenied()
        {
            //show page if user is unauthorized to view page
            ViewData["Genres"] = GetGenres();
            return View();
        }
    }
}