using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public List<BookOrderViewModel> Books { get; set; }
        public OrderDetail OrderDetails { get; set; }
    }
}