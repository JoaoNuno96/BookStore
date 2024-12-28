using BookStore.Data;
using BookStore.Models.Entities;
using BookStore.Models.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Models.Services
{
    public class BookService
    {
        private readonly BookStoreContext _context;
        public BookService(BookStoreContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public List<Book> FindAllBooks()
        {
            return this._context.Book.Include(x => x.Category)
                                     .Include(x => x.Publisher)
                                     .Include(x => x.Author)
                                     .OrderBy(x => x.Id)
                                     .ToList();
        }
        [HttpGet("{id}")]
        public Book FindBookById(int id)
        {
            return this._context.Book.FirstOrDefault(x => x.Id == id);
        }

        public void AddBook(Book book)
        {
            this._context.Add(book);
            this._context.SaveChanges();
        }

        public void Update(Book book)
        {
            if(!this._context.Book.Any(x => x.Id == book.Id))
            {
                throw new NotFoundException("Id not Found");
            }

            try
            {
                this._context.Update(book);
                this._context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }

        public void RemoveBook(int id)
        {
            Book book = this.FindBookById(id);
            this._context.Remove(book);
            this._context.SaveChanges();
        }

    }
}
