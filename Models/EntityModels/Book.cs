using System;

namespace BookCave.Models.EntityModels
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int[] Genre { get; set; }
        public string Info { get; set; }
        public int AuthorId { get; set; }
        public string Publisher { get; set; }
        public int PageCount { get; set; }
        public int ReleaseYear { get; set; }
        public double Price { get; set; }
        public double Discount  { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
        public int Stock { get; set; }
    }
}