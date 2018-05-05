using BookCave.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class CartController : Controller
    {
        private CartService _cartService;

        public CartController() //Constructor
        {
            _cartService = new CartService();
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Cart";
            return View();
        }
    }
}