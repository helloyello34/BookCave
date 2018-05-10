using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;
using System;
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

        public void AddItemToCart(int id, string userId)
        {
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == userId
                && c.BookId == id);

                if(cartItem == null)
                {
                    cartItem = new BookCave.Models.EntityModels.Cart
                    {
                        BookId = id,
                        CartId = userId,
                        Count = 1,
                        DateCreated = DateTime.Now
                    };
                    _db.Carts.Add(cartItem);
                }
                else
                {
                    cartItem.Count++;
                }
                _db.SaveChanges();
        }

        public int RemoveFromCart(int id, string userId)
        {
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == userId
                && c.BookId == id);

            int itemCount = 0;
            if(cartItem != null)
            {
                if(cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _db.Carts.Remove(cartItem);
                }
                _db.SaveChanges();
            }
            return itemCount;
        }

        public CartViewModel GetCart(string userId)
        {
           var cartItems =  _cartRepo.GetCartItems(userId);
           var cVM = new CartViewModel{
               CartId = userId,
               CartItems = cartItems
           };

            return cVM;
        }
    }
}