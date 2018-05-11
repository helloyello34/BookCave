namespace BookCave.Models.ViewModels
{
    public class BookOrderViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}