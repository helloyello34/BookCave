namespace BookCave.Models.EntityModels
{
    public class CartItem 
    {
        public int CartItemId { get; set; }
        public Book Book { get; set; }
        public double Amount { get; set; }
        public string CartId { get; set; }
    }

}