using BookStore.Data;
using BookStore.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Services
{
    public class CategoryService
    {
        private readonly BookStoreContext _context;
        public CategoryService(BookStoreContext contx)
        {
            this._context = contx;
        }
        public List<Category> GetCategories()
        {
            return this._context.Category.ToList();
        }
    }
}
