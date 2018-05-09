using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BookCave.Models.EntityModels
{
    public class Cart
    {
        private readonly DataContext _db;
        private readonly UserManager<ApplicationUser> _userManager;


        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }


        public void AddToCart(Book book, int amount)
        {
            var cartItem = 
                _db.CartItems.SingleOrDefault(
                    s => s.Book.Id == book.Id && s.CartId == CartId);
            if(cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartId = CartId,
                    Book = book,
                    Amount = 1
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Amount++;
            }
            _db.SaveChanges();
        }
    }


}