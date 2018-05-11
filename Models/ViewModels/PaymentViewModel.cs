using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class PaymentViewModel
    {
        public List<CartItemsViewModel> Items { get; set; }
        public decimal Total { get; set; }   
    }
}