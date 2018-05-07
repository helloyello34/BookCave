using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;

        public CartService()
        {
            _cartRepo = new CartRepo();
        }
    }
}