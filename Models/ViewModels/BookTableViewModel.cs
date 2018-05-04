namespace BookCave.Models.ViewModels
{
    public class BookTableViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public double Discount { get; set; }
    }
}