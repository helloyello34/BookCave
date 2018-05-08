using System;
using System.Collections.Generic;
using System.Linq;
using BookCave.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BookCave.Models.EntityModels
{
    public class Cart
    {
        private readonly DataContext _db;
        private Cart(DataContext db)
        {
            _db = db;
        }
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public double Price { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

                var context = services.GetService<DataContext>();
                string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

                session.SetString("CartId", cartId);

                return new Cart(context) {CartId = cartId};
        }

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