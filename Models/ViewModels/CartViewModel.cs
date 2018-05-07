using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<Book> BookList { get; set; }
        public double Price { get; set; }
    }
}