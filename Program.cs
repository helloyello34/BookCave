using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Models.EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        public static void SeedData()
        {
            var db = new DataContext();

            if(!db.Books.Any())
            {
                var initialBooks = new List<Book>()
                {
                    new Book { ISBN = "	0-7475-3269-9", Language = "English", Image = "https://upload.wikimedia.org/wikipedia/en/6/6b/Harry_Potter_and_the_Philosopher%27s_Stone_Book_Cover.jpg",
                     Title = "Harry Potter and the Philosopher's Stone", Genre = "Fantasy", Info = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Optio voluptas laudantium nobis similique rerum nihil animi perferendis ipsam incidunt. Nemo asperiores quos aliquam hic. Obcaecati in quis ex! Pariatur possimus quam sit nemo, accusantium, ex, illo sequi unde cumque eveniet quibusdam! Cumque consequuntur, dicta id, mollitia aliquam et perspiciatis quidem quae obcaecati minus, nam dolor dolore nulla nisi magnam sint voluptas! Ratione officia deleniti dolorum obcaecati! Incidunt perferendis cumque tempora explicabo asperiores magni, ipsa eligendi. Porro tempora beatae consequatur eligendi? Molestiae nobis eius rem eligendi facere fuga temporibus rerum cum? Ut vitae alias minus exercitationem voluptas maiores fugiat vero placeat rerum quas. Aliquid nostrum totam ad odit quibusdam, suscipit ipsam ea expedita soluta voluptates magni necessitatibus praesentium tenetur ipsa perferendis adipisci iste repellat quos alias esse! Aliquid neque ad, culpa provident minima dolor quibusdam eius libero deleniti sunt nobis tenetur iusto rerum nulla architecto, mollitia vero, recusandae sit. Voluptatum repellendus accusamus illum blanditiis eveniet, dolorem quibusdam excepturi expedita nesciunt provident in eum ullam impedit voluptas ducimus qui placeat fugit veritatis. Cumque commodi eum nemo laudantium. Ullam officiis ab repellat quas sint accusamus earum, magnam id laboriosam rem, quam natus nam odio et provident dicta nemo reprehenderit cupiditate obcaecati aspernatur recusandae.",
                      AuthorId = 1, Publisher = "Bloomsbury", PageCount = 223, ReleaseYear = 1997, price = 39.99, Discount = 0, Rating = 0, RatingCount = 0, Stock = 10},
                    new Book { ISBN = "	0-7475-3849-2", Language = "English", Image = "https://upload.wikimedia.org/wikipedia/en/5/5c/Harry_Potter_and_the_Chamber_of_Secrets.jpg",
                     Title = "Harry Potter and the chamber of secrets", Genre = "Fantasy", Info = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Optio voluptas laudantium nobis similique rerum nihil animi perferendis ipsam incidunt. Nemo asperiores quos aliquam hic. Obcaecati in quis ex! Pariatur possimus quam sit nemo, accusantium, ex, illo sequi unde cumque eveniet quibusdam! Cumque consequuntur, dicta id, mollitia aliquam et perspiciatis quidem quae obcaecati minus, nam dolor dolore nulla nisi magnam sint voluptas! Ratione officia deleniti dolorum obcaecati! Incidunt perferendis cumque tempora explicabo asperiores magni, ipsa eligendi. Porro tempora beatae consequatur eligendi? Molestiae nobis eius rem eligendi facere fuga temporibus rerum cum? Ut vitae alias minus exercitationem voluptas maiores fugiat vero placeat rerum quas. Aliquid nostrum totam ad odit quibusdam, suscipit ipsam ea expedita soluta voluptates magni necessitatibus praesentium tenetur ipsa perferendis adipisci iste repellat quos alias esse! Aliquid neque ad, culpa provident minima dolor quibusdam eius libero deleniti sunt nobis tenetur iusto rerum nulla architecto, mollitia vero, recusandae sit. Voluptatum repellendus accusamus illum blanditiis eveniet, dolorem quibusdam excepturi expedita nesciunt provident in eum ullam impedit voluptas ducimus qui placeat fugit veritatis. Cumque commodi eum nemo laudantium. Ullam officiis ab repellat quas sint accusamus earum, magnam id laboriosam rem, quam natus nam odio et provident dicta nemo reprehenderit cupiditate obcaecati aspernatur recusandae.",
                      AuthorId = 1, Publisher = "Bloomsbury", PageCount = 251, ReleaseYear = 1998, price = 39.99, Discount = 0, Rating = 0, RatingCount = 0, Stock = 10}
                };
                var initialAuthors = new Author { Name = "J.K Rowling"};

                db.Add(initialAuthors);
                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }

    }
}