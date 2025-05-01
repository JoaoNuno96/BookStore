using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Entities;
using BookStore.Models.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIAuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public APIAuthorController(AuthorService authorServiceParam)
        {
            this._authorService = authorServiceParam;
        }

        //GET ALL
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            return await this._authorService.GetAuthorsAsync();
        }
    }
}
