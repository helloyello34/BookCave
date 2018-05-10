using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.EntityModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace BookCave.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private CartService _cartService;
        private ShoppingCart _shoppingCart;
        
    public CartController(UserManager<ApplicationUser> usermanager)
    {
        _userManager = usermanager;
        //var user = _userManager.GetUserAsync(User);
       // var userId = user.Id.ToString();
        _shoppingCart = new ShoppingCart();
        _cartService = new CartService();
    }
        public async Task<string> GetCartId()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            return userId;
        }

        public async Task AddBook(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.AddItemToCart(id, userId);
        }

        public async Task RemoveBook(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.RemoveFromCart(id, userId);
        }
    }
}