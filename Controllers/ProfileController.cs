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

namespace BookCave.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCrurentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Home()
        {
            var user = await GetCrurentUserAsync();

            var profile = new ProfileHomeViewModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                StreetAddress = user.Street,
                ZipCode = user.ZipCode,
                City = user.City,
                Country = user.Country,
                ImageUrl = user.ImageUrl
            };

            return View(profile);
        }

        public async Task<IActionResult> EditPersonal()
        {
            var user = await GetCrurentUserAsync();
            var person = new UserPersonalInputModel {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Birthday = user.Birthday
            };

            var image = user.ImageUrl;
            ViewData["Image"] = image;

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonal(UserPersonalInputModel model)
        {
            if( ModelState.IsValid )
            {
                var user = await GetCrurentUserAsync();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Gender = model.Gender;
                user.ImageUrl = model.ImageUrl;
                user.Birthday = model.Birthday;

                await _userManager.UpdateAsync(user);

                return RedirectToAction("Home", "Profile");
            }
            return View();
        }

        public async Task<IActionResult> EditShipping()
        {
            var user = await GetCrurentUserAsync();
            var person = new UserShippingInputModel {
                StreetAddress = user.Street,
                ZipCode = user.ZipCode,
                City = user.City,
                Country = user.Country
            };
            var image = user.ImageUrl;
            ViewData["Image"] = image; 

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> EditShipping( UserShippingInputModel model )
        {

            if( ModelState.IsValid )
            {
                var user = await GetCrurentUserAsync();
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