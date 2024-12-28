using BookStore.Data;
using System.Collections.Generic;
using BookStore.Models.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Models.Services
{
    public class AuthorService
    {
        private readonly BookStoreContext _context;
        public AuthorService(BookStoreContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public List<Author> GetAuthors()
        {
            return this._context.Author.ToList();
        }
    }
}
