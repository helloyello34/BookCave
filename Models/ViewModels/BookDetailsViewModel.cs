using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }
        public string Info { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int[] Genre { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}