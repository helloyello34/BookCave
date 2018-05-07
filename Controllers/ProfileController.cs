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
                Country = user.Country
            };

            return View(profile);
        }

        public async Task<IActionResult> EditPersonal()
        {
            var user = await GetCrurentUserAsync();
            var person = new UserPersonalInputModel {
                FirstName = user.FirstName,
                LastName = user.LastName
            };



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
                
                await _userManager.UpdateAsync(user);

                var userr = User as ClaimsPrincipal;
                var identity = userr.Identity as ClaimsIdentity;
                var claim = (
                    from c in userr.Claims
                    where c.Type == "FullName"
                    select c).Single();
                identity.RemoveClaim(claim);
                identity.AddClaim(new Claim("FullName", $"{model.FirstName} {model.LastName}"));
                // await _userManager.AddClaimAsync(user, new Claim("FullName", $"{model.FirstName} {model.LastName}"));

                
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