using BookCave.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookCave.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var book = new BookDetailsViewModel {
                Title = "Title!",
                Author = "Author!",
                ReleaseYear = 1111,
                Rating = 4.5,
                Info = "Info on book",
                Price = 49.99,
                Image = "https://www.google.is/imgres?imgurl=https://www.guida-vino.com/images/stories/virtuemart/product/Sauvignon1.jpg&imgrefurl=https://www.guida-vino.com/it/vini-doc-e-docg-del-friuli-venezia-giulia/vendita-online-vino-collio-goriziano-doc/item/476-vino-collio-doc-o-collio-goriziano-doc.html&h=703&w=703&tbnid=krj9V-jao_clwM:&tbnh=160&tbnw=160&usg=__hWQb_uwTklsV0tJTrSvQyY9GD2k%3D&vet=1&docid=goAaddxFkdXUZM&itg=1&client=ubuntu&sa=X&ved=0ahUKEwiG5KqWu-faAhVDG5oKHaHHBVoQ_B0IlQEwCg",
                Genre = "Horror",
                Language = "polish",
                Format = "scroll"
            };
            return View(book);
        }
    }
}