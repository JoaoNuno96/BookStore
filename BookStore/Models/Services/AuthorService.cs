using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Services
{
    public class AuthorService
    {
        private readonly BookStoreContext _context;
        public AuthorService(BookStoreContext context)
        {
            this._context = context;
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            return await this._context.Author.ToListAsync();
        }

        public async Task AddAuthorAsync(Author au)
        {
            await this._context.Author.AddAsync(au);
            await this._context.SaveChangesAsync();
        }

        public async Task<Author> FindAuthorByIdAsync(int id)
        {
            return await this._context.Author.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task EditAuthorAsync(Author au)
        {
            this._context.Author.Update(au);
            await this._context.SaveChangesAsync();
        }

        public async Task RemoveAuthorAsync(int id)
        {
            Author au = await this.FindAuthorByIdAsync(id);
            this._context.Author.Remove(au);
            await this._context.SaveChangesAsync();
        }

        public async Task CreateNewAuthor(Author form)
        {
            if (form == null) throw new Exception("No object of creating has been send!");

            this._context.Author.Add(form);
            await this._context.SaveChangesAsync();
        }
    }
}
