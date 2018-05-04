using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class BookTopTenViewModel
    {
        public List<BookTableViewModel> FirstHalf { get; set;}
        public List<BookTableViewModel> SecondHalf { get; set;}
    }
}