using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.Entities;
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
    }
}
