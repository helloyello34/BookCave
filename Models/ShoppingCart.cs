using BookCave.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
namespace BookCave.Models.EntityModels
{
    public partial class ShoppingCart
    {
        public ShoppingCart(string userId)
        {
            ShoppingCartId = userId;
        }
        public ShoppingCart()
        {
            
        }
        
        DataContext _db = new DataContext();

        string ShoppingCartId { get; set; }
       
        public ShoppingCart GetCart(string id)
        {
            var cart = new ShoppingCart(ShoppingCartId);
            return cart;
        }

        public void AddToCart(Book book)
        {
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.BookId == book.Id);

                if(cartItem == null)
                {
                    cartItem = new Cart
                    {
                        BookId = book.Id,
                        CartId = ShoppingCartId,
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

        public int RemoveFromCart(int id)
        {
            var cartItem = _db.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.Id == id);

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

        public void EmptyCart()
        {
            var cartItems = _db.Carts.Where(
                cart => cart.CartId == ShoppingCartId);
            
            foreach (var cartItem in cartItems)
            {
                _db.Carts.Remove(cartItem);
            }
            _db.SaveChanges();
        }
        
        public List<Cart> GetCartItems()
        {
            return _db.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            int? count = (from cartItems in _db.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            return count ?? 0;
        }

        public double GetTotal()
        {
            double? total = (from cartItems in _db.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Book.Price).Sum();

            return total ?? 0;
        }

        public int CreateOrder(Order order)
        {
            double orderTotal = 0;
            var cartItems = GetCartItems();
            foreach(var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    BookId = item.BookId,
                    OrderId = order.OrderId,
                    UnitPrice = (decimal)item.Book.Price,
                    Quantity = item.Count
                };
                orderTotal += (item.Count * item.Book.Price);
                _db.OrderDetails.Add(orderDetail);
            }
            order.Total = (decimal)orderTotal;
            _db.SaveChanges();

            EmptyCart();
            return order.OrderId;
        }

       /* public string GetCartId()
        {
            var userId = _userManager.GetUserId;
        }
*/
        public void MigrateCart(string userName)
        {
            var shoppingCart = _db.Carts.Where(
                c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _db.SaveChanges();
        }
    }
}