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
                Info = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Optio voluptas laudantium nobis similique rerum nihil animi perferendis ipsam incidunt. Nemo asperiores quos aliquam hic. Obcaecati in quis ex! Pariatur possimus quam sit nemo, accusantium, ex, illo sequi unde cumque eveniet quibusdam! Cumque consequuntur, dicta id, mollitia aliquam et perspiciatis quidem quae obcaecati minus, nam dolor dolore nulla nisi magnam sint voluptas! Ratione officia deleniti dolorum obcaecati! Incidunt perferendis cumque tempora explicabo asperiores magni, ipsa eligendi. Porro tempora beatae consequatur eligendi? Molestiae nobis eius rem eligendi facere fuga temporibus rerum cum? Ut vitae alias minus exercitationem voluptas maiores fugiat vero placeat rerum quas. Aliquid nostrum totam ad odit quibusdam, suscipit ipsam ea expedita soluta voluptates magni necessitatibus praesentium tenetur ipsa perferendis adipisci iste repellat quos alias esse! Aliquid neque ad, culpa provident minima dolor quibusdam eius libero deleniti sunt nobis tenetur iusto rerum nulla architecto, mollitia vero, recusandae sit. Voluptatum repellendus accusamus illum blanditiis eveniet, dolorem quibusdam excepturi expedita nesciunt provident in eum ullam impedit voluptas ducimus qui placeat fugit veritatis. Cumque commodi eum nemo laudantium. Ullam officiis ab repellat quas sint accusamus earum, magnam id laboriosam rem, quam natus nam odio et provident dicta nemo reprehenderit cupiditate obcaecati aspernatur recusandae.",
                Price = 49.99,
                Image = "http://c0d3.attorney/_1.php?m=863",
                Genre = "Horror",
                Language = "polish",
                Format = "scroll"
            };
            return View(book);
        }
    }
}