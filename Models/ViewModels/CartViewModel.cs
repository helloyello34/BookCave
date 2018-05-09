using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public double CartTotal { get; set; }
    }
}