using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;
        private DataContext _db;

        public CartService()
        {
            _cartRepo = new CartRepo();
            _db = new DataContext();
        }

        public void AddItemToCart(int id)
        {
            var cartItem = (
                from b in _db.Books
                where b.Id == id
                select new CartItem
                {
                    Book = b,
                    Amount = b.Price
                }).SingleOrDefault();
            _cartRepo.AddBookToDb(cartItem);
        }

        public List<CartItem> GetItems(string id)
        {
            var items = _cartRepo.GetItems(id);
            return items;
        }
    }
}