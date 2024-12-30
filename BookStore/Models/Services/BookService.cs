using BookStore.Data;
using BookStore.Models.Entities;
using BookStore.Models.Services.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.EntityFrameworkCore.Internal;

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

        public async Task<List<Book>> FindAllBooksAsync()
        {
            return await this._context.Book.Include(x => x.Category)
                                     .Include(x => x.Publisher)
                                     .Include(x => x.Author)
                                     .OrderBy(x => x.Id)
                                     .ToListAsync();
        }

        public async Task<Book> FindBookByIdAsync(int id)
        {
            return await this._context.Book.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddBookAsync(Book book)
        {
            this._context.Add(book);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            if(!await this._context.Book.AnyAsync(x => x.Id == book.Id))
            {
                throw new NotFoundException("Id not Found");
            }

            try
            {
                this._context.Update(book);
                await this._context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }

        public async Task RemoveBookAsync(int id)
        {
            Book book = await this.FindBookByIdAsync(id);
            this._context.Remove(book);
            await this._context.SaveChangesAsync();
        }

    }
}
