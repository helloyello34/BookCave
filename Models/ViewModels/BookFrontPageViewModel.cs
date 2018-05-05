using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class BookFrontPageViewModel
    {
        public List<BookTableViewModel> PopularBooks { get; set;}
        public List<BookTableViewModel> RecentlyAddedBooks { get; set;}
        public List<BookTableViewModel> RandomBooks { get; set;}
    }
}