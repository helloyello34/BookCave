using System;
using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.EntityModels;
using System.Linq;


namespace BookCave.Repositories
{
    public class CartRepo
    {
        private DataContext _db;

        public CartRepo()
        {
            _db = new DataContext();
        }
        public void AddBookToDb(CartItem cartItem)
        {
        }

        public List<Cart> GetCartItems(string userId)
        {
            var cartItems = (
                from i in _db.Carts
                join b in _db.Books on i.BookId equals b.Id
                where i.CartId == userId
                select new Cart {
                    Book = b,
                    CartId = i.CartId,
                    Count = i.Count,
                    BookId = i.BookId,
                    DateCreated = i.DateCreated
                }
                ).ToList();

                return cartItems;
        }

       
    }
}