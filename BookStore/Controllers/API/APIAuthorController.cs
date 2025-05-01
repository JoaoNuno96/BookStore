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

        //GET SINGLE
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Author>> GetSingleAuthor(int id)
        {
            return await this._authorService.FindAuthorByIdAsync(id);
        }

        //EDIT
        [HttpPost("edit/author")]
        public async Task<ActionResult<bool>> UpdateAuthor([FromBody] Author authParam)
        {
            //Verfiy If author is null
            if (authParam == null)
            {
                return NotFound(false);
            }
            else
            {
                await this._authorService.EditAuthorAsync(authParam);
                return Ok(true);
            }
        }
    }
}
