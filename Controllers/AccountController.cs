using System.Security.Claims;
using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.ViewModels;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task CreateAdmin()
        {
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
                    //Admin gefið role
                    await _userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }         
        } 
       public async Task<IActionResult> Login()
        {
            //Þetta fall er kallað í til að bua til admin user
            //utaf það virkaði ekki inn í startup.cs
            await CreateAdmin();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) { return View(); }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if ( result.Succeeded )
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if ( !ModelState.IsValid ) { return View(); }

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

            var result = await _userManager.CreateAsync(user, model.Password);

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
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}