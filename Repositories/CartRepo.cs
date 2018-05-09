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

        public List<CartItem> GetItems(string id)
        {
            var items = ( 
                from i in _db.CartItems
                where i.CartId == id
                select i).ToList();
            return items;
        }
    }
}