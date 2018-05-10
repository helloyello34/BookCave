using System.Collections.Generic;
using System.Threading.Tasks;
using BookCave.Models;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
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
        private BookService _bookService;
        private ShoppingCart _shoppingCart;
        
        public CartController(UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            //var user = _userManager.GetUserAsync(User);
        // var userId = user.Id.ToString();
            _shoppingCart = new ShoppingCart();
            _cartService = new CartService();
            _bookService = new BookService();
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Your cart:";
            ViewData["Genres"] = GetGenres();
            var userId = await GetCartId();
            var cart = GetCart(userId);
            if(cart.CartItems.Count == 0)
            {
                ViewData["Title"] = "You have no items in your cart";
            }
            return View(cart);
        }

        public List<string> GetGenres()
        {
            var genres = _bookService.GetGenresList();
            return genres;            
        }   
        public async Task<string> GetCartId()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            return userId;
        }

        public CartViewModel GetCart(string id)
        {
            var cVM = _cartService.GetCart(id);
            return cVM;

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

        public async Task EmptyCart()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.EmptyCart(userId);
            
        }

        public async Task GetTotal()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.GetTotal(userId);
            
        }

        public async Task CreateOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var order = _cartService.MakeNewOrder(userId);
            order.Address = user.Street;
            order.City = user.City;
            order.Country = user.Country;
            order.Email = user.Email;
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.OrderDate = System.DateTime.Now;

            _cartService.CreateOrder(order,userId);
        } 
        public async Task<IActionResult> Order()
        {
            var user = await _userManager.GetUserAsync(User);
            
            var order = new OrderViewModel {
                Id = user.Id
            };

            return View(order);
            
        }

        public async Task GetOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            var orders = _cartService.GetOrders(userId);
        }
    }
}