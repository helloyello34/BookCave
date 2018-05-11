using System;
using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.EntityModels;
using System.Linq;
using BookCave.Models.ViewModels;

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

        public List<CartItemsViewModel> GetCartItems(string userId)
        {
            var cartItems = (
                from i in _db.Carts
                join b in _db.Books on i.BookId equals b.Id
                join a in _db.Authors on i.Book.AuthorId equals a.Id
                where i.CartId == userId
                select new CartItemsViewModel {
                    Book = b,
                    CartId = i.CartId,
                    Count = i.Count,
                    BookId = i.BookId,
                    DateCreated = i.DateCreated,
                    AuthorName = a.Name 
                }
                ).ToList();

                return cartItems;
        }

        public Order MakeNewOrder(string userId)
        {
            var newOrder = new Order
            {
                UserId = userId
            };
            _db.Orders.Add(newOrder);
            _db.SaveChanges();

            return newOrder;
        }
        
        public List<Order> GetOrders(string userId)
        {
            var orders = (
                from o in _db.Orders
                where o.UserId == userId
                select o).ToList();
                
                foreach (var o in orders)
                {
                    var orderDetails = (
                        from od in _db.OrderDetails
                        where od.IdOfOrder == o.OrderId
                        select od).ToList();

                    o.OrderDetails = orderDetails;
                }
            return orders;
        }

       public List<OrderDetail> GetOrderDetails(int orderId)
       {
           var orderDetails = (
               from o in _db.OrderDetails
               where o.IdOfOrder == orderId
               join b in _db.Books on o.BookId equals b.Id
               select new OrderDetail
               {
                   Book = b,
                   BookId = o.BookId,
                   Quantity = o.Quantity,
                   UnitPrice = o.UnitPrice,
                   UserId = o.UserId}).ToList();

            return orderDetails;
       }
    }
}