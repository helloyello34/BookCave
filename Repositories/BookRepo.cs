using BookCave.Data;
using BookCave.Models.EntityModels;
using BookCave.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;
        public BookRepo()
        {
            _db = new DataContext();
        }
        public BookDetailsViewModel GetBookById(int id)
        {
            var book = (
                from b in _db.Books
                join a in _db.Authors on b.AuthorId equals a.Id
                where b.Id == id
                select new BookDetailsViewModel {

                    Title = b.Title,
                    Author = a.Name,
                    ReleaseYear = b.ReleaseYear,
                    Rating = b.Rating,
                    Info = b.Info,
                    Price = b.Price,
                    Image = b.Image,
                    Genre = b.Genre,
                    Language = b.Language,
                    Publisher = b.Publisher,
                    ISBN = b.ISBN
                }).SingleOrDefault();

            return book;
        }

        public void AddBook(Book book)
        {
            _db.Add(book);
            _db.SaveChanges();
        }

        public void AddAuthor(Author author)
        {
            _db.Add(author);
            _db.SaveChanges();
            Console.Write("Book added");
        }

        public List<AuthorsViewModel> GetAuthorList()
        {
            var authorList = (
                from a in _db.Authors
                select new AuthorsViewModel {
                    Id = a.Id,
                    Name = a.Name
                    }).ToList();
                return authorList;
        }
      /*  public int GetNewBookId()
        {
            var id = 0;
                foreach (var m in _db.Books)
                {
                    if(id < m.Id)   
                    {
                        id = m.Id;
                    };
                }
                return id + 1;
        }*/
    }
}