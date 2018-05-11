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

        public void EmptyCart(string userId)
        {
            var cartItems = (
                from i in _db.Carts
                where i.CartId == userId
                select i).ToList();
 
            foreach (var item in cartItems)
            {
                _db.Carts.Remove(item);
            }
            // Save changes
            _db.SaveChanges();
        }

        public double GetTotal(string userId)
        {
            double? total = (from cartItems in _db.Carts
                              where cartItems.CartId == userId
                              select (int?)cartItems.Count *
                              cartItems.Book.Price).Sum();

            return total ?? 0;
        }

        public int CreateOrder(Order order, string userId)
        {
            decimal orderTotal = 0;
 
            var cartItems = _cartRepo.GetCartItems(userId);
            
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    UserId = userId,
                    BookId = item.BookId,
                    IdOfOrder = order.OrderId,
                    UnitPrice = (decimal)item.Book.Price,
                    Quantity = item.Count
                };
                orderTotal += (item.Count * (decimal)item.Book.Price);
                _db.OrderDetails.Add(orderDetail);
                _db.Orders.Update(order);
            }
            order.Total = orderTotal;
            _db.SaveChanges();
            EmptyCart(userId);
            return order.OrderId; ///order.OrderId;
        }
        
        public Order MakeNewOrder(string userId)
        {
            Order newOrder = _cartRepo.MakeNewOrder(userId);
            return newOrder;
        }

        public List<Order> GetOrders(string userId)
        {
            var orders = _cartRepo.GetOrders(userId);

            return orders;
        }

        public List<OrderDetail> GetOrderDetails(int orderId)
        {
            var orderDetails = _cartRepo.GetOrderDetails(orderId);

            return orderDetails;
        }
    }
}