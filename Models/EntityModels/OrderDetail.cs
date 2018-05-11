using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.EntityModels
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public string UserId { get; set; }
        public int IdOfOrder { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
    }
}