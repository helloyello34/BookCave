using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.EntityModels;
using BookCave.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class CartController : Controller
    {
        private CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

       private Task<ApplicationUser> GetCrurentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CartController(Cart cart) //Constructor
        {
            _cartService = new CartService();
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["Title"] = "Your shopping cart";
            var cart = new Cart{
            CartId = user.Id,
            CartItems = (_cartService.GetItems(user.Id))
            };

            var cartVM = new CartViewModel{
                Cart = cart;
            }

            return View(cart);
        }


        [HttpPost]
        public IActionResult AddItemToCart(int id)
        {
            _cartService.AddItemToCart(id);
            return View();
        }

        
    }
}