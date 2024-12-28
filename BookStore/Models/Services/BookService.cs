using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;

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
            return this._context.Book.Include(x => x.Category)
                                     .Include(x => x.Publisher)
                                     .Include(x => x.Author)
                                     .OrderBy(x => x.Id)
                                     .ToList();
        }
        public Book FindBookById(int id)
        {
            return this._context.Book.FirstOrDefault(x => x.Id == id);
        }

        public void AddBook(Book book)
        {
            this._context.Add(book);
            this._context.SaveChanges();
        }

        public void RemoveBook(int id)
        {
            Book book = this.FindBookById(id);
            this._context.Remove(book);
            this._context.SaveChanges();
        }

    }
}
