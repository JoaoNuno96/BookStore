using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using BookStore.Models.Services;

namespace BookStore.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class APICategoryController : ControllerBase
    {
        private readonly CategoryService _catService;

        public APICategoryController(CategoryService catService)
        {
            this._catService = catService;
        }

        //GET ALL
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Category>>> RecoverAllCat()
        {
            return await this._catService.GetCategoriesAsync();
        }
    }
}
