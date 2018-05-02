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
        public string Genre { get; set; }
        public string Info { get; set; }
        public int AuthorId { get; set; }
        public int PageCount { get; set; }
        public int ReleaseYear { get; set; }
        public double price { get; set; }
        public double Discount  { get; set; }
        public double Rating { get; set; }
        public uint RatingCount { get; set; }
        public uint Stock { get; set; }
    }
}