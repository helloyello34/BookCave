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

namespace BookCave.Controllers
{
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
                Country = user.Country
            };

            return View(profile);
        }

        public async Task<IActionResult> EditPersonal()
        {
            var user = await GetCrurentUserAsync();
            var personal = new UserPersonalInputModel {
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            return View(personal);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonal(UserPersonalInputModel model)
        {
            var user = await GetCrurentUserAsync();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Home", "Profile");
        }
        // [HttpPost]
        // public async Task<IActionResult> Home(UserInputModel model)
        // {
        //     var user = await GetCrurentUserAsync();
        //     user.FirstName = model.FirstName;
        //     user.LastName = model.LastName;
            
        //     await _userManager.UpdateAsync(user);

        //     return View();
        // }
    }
}