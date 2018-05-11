using BookCave.Models.EntityModels;

namespace BookCave.Models.ViewModels
{
    public class CartItemsViewModel
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public int BookId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Book Book { get; set; }
        public string AuthorName { get; set; }
    }
}