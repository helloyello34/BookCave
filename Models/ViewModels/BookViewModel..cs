using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class Cart
    {
        public List<Book> BookList { get; set; }
        public double Price { get; set; }
    }
}