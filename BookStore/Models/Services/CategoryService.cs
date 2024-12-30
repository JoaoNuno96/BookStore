using BookStore.Data;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Services
{
    public class CategoryService
    {
        private readonly BookStoreContext _context;
        public CategoryService(BookStoreContext contx)
        {
            this._context = contx;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await this._context.Category.ToListAsync();
        }
    }
}
