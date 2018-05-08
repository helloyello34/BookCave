using BookCave.Models.EntityModels;
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
            ViewData["Title"] = "Your shopping cart";
            return View();
        }
        [HttpPost]
        public IActionResult AddItemToCart(int id)
        {
            _cartService.AddItemToCart(id);
            return View();
        }

        
    }
}