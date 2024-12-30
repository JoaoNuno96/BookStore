using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Services;
using BookStore.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _catservice; 

        public CategoryController(CategoryService ct)
        {
            this._catservice = ct;
        }

        public async Task<IActionResult> Index()
        {
            List<Category> cat = await this._catservice.GetCategoriesAsync();

            return View(cat);
        }
    }
}
