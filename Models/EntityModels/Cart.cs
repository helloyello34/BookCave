using System.Collections.Generic;

namespace BookCave.Models.EntityModels
{
    public class Cart
    {
        public string CartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public double Price { get; set; }
    }
}