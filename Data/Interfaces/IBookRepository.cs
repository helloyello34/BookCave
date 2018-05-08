using System.Collections.Generic;
using BookCave.Models.EntityModels;

namespace BookCave.Data.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; set; }
        Book GetBookById(int id);
    }
}