using BookStore.Data;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task CreateCategoryAsync(Category ca)
        {
            if (ca == null) throw new Exception("Object category is null");

            await this._context.Category.AddAsync(ca);
            await this._context.SaveChangesAsync();
        }
        public async Task<Category> GetCategoryById(int id)
        {
            Category cat = await this._context.Category
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Category not found!"); ;

            return cat;
        }

        public async Task EditCategoryAsync(int id, Category ca)
        {
            if(ca == null) throw new Exception("Category is null");

            Category category = await this._context.Category.FirstOrDefaultAsync(x => x.Id == id);

            category.Nome = ca.Nome;

            await this._context.SaveChangesAsync();
        }

        public async Task RemoveCategoryAsync(int id)
        {
            Category ca = await this.GetCategoryById(id);
            this._context.Category.Remove(ca);
            await this._context.SaveChangesAsync();
        }
    }
}
