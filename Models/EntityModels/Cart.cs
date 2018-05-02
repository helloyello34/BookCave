using System.Collections.Generic;

namespace BookCave.Models.EntityModels
{
    public class Cart
    {
        public int ID { get; set; }
        public List<Book> BookList { get; set; }
        public double Price { get; set; }
    }
}