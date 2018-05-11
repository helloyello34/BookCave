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
        
        public CartController(UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
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
            var order = _cartService.MakeNewOrder( userId );
            order.Address = user.Street;
            order.City = user.City;
            order.Country = user.Country;
            order.Email = user.Email;
            order.FirstName = user.FirstName;
            order.LastName = user.LastName;
            order.OrderDate = System.DateTime.Now;

            _cartService.CreateOrder( order, userId );
        } 
       /* public async Task<IActionResult> Checkout()
        {
            ViewData["Genres"] = GetGenres();
            var user = await _userManager.GetUserAsync(User);
            var userId = await GetCartId();
            var cart = GetCart(userId);

            return View(order);
            
        }*/

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var orders = _cartService.GetOrders(userId);
            // var viewInPayment = new PaymentViewModel {
            //     foreach (var Book in collection)
            //     {
                    
            //     }
            // }
            

            return orders;
        }

        public async Task<List<OrderDetail>> GetOrderDetails(int orderId)
        {
             var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var orderDetails = _cartService.GetOrderDetails(orderId);
            return orderDetails;
        }

        public async Task<IActionResult> OrderOverView()
        {
            ViewData["Genres"] = GetGenres();
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var orders = await GetOrders();
            
            return View(orders);
        }
    }
}