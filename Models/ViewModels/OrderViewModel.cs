using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class OrderViewModel
    {
        public string Id         { get; set; }
        public string FirstName  { get; set; }
        public string LastName   { get; set; }
        public string Address    { get; set; }
        public string City       { get; set; }
        public string State      { get; set; }
        public string PostalCode { get; set; }
        public string Country    { get; set; }
        public string Email      { get; set; }
        public decimal Total     { get; set; }
        public List<BookOrderViewModel> Books        { get; set; }
        public List<OrderDetail>        OrderDetails { get; set; }
    }
}