using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.Entities;
using System.Linq;

namespace BookStore.Models.Services
{
    public class BookService
    {
        private readonly BookStoreContext _context;
        public BookService(BookStoreContext context)
        {
            this._context = context;
        }
        public List<Book> FindAllBooks()
        {
            return this._context.Book.ToList();
        }
    }
}
