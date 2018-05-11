using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public string CartId { get; set; }
        public List<CartItemsViewModel> CartItems { get; set; }
        


    }
}