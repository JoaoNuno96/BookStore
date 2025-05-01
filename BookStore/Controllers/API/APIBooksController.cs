using BookStore.Models.Services;
using BookStore.Models.Services.Exceptions;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace BookStore.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIBooksController : ControllerBase
    {
        private readonly BookService _bookservice;
        private readonly AuthorService _authorservice;
        private readonly CategoryService _categoryservice;
        private readonly PublisherService _publisherservice;

        public APIBooksController(BookService bookservice, AuthorService aus, CategoryService cs, PublisherService ps)
        {
            this._bookservice = bookservice;
            this._authorservice = aus;
            this._categoryservice = cs;
            this._publisherservice = ps;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult<List<Book>>> RecoverBooks()
        {
            List<Book> listBooks = await this._bookservice.FindAllBooksAsync();

            if(listBooks != null)
            {
                return Ok(listBooks);
            }
            else
            {
                return NotFound("No book found");
            }
        }

        [HttpGet("getViewModel")]
        public async Task<ActionResult<BookFormViewModel>> CreateViewModelForm()
        {
            List<Category> listCategories = await this._categoryservice.GetCategoriesAsync();
            List<Author> listAuthors = await this._authorservice.GetAuthorsAsync();
            List<Publisher> listPublishers = await this._publisherservice.GetPublishersAsync();

            BookFormViewModel model = new BookFormViewModel
            {
                Authors = listAuthors,
                Categories = listCategories,
                Publishers = listPublishers
            };

            if(listCategories != null && listAuthors !=null && listPublishers != null)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest("Something went wrong in request!");
            }

        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Book>> Details(int id)
        {
            Book? book = await this._bookservice.FindBookByIdAsync(id);

            if(book != null)
            {
                return Ok(book);
            }
            else
            {
                return NotFound("Book not found");
            }
        }

        [HttpPost("create/book")]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                List<Category> listCategories = await this._categoryservice.GetCategoriesAsync();
                List<Author> listAuthors = await this._authorservice.GetAuthorsAsync();
                List<Publisher> listPublishers = await this._publisherservice.GetPublishersAsync();

                BookFormViewModel model = new BookFormViewModel
                {
                    Authors = listAuthors,
                    Categories = listCategories,
                    Publishers = listPublishers
                };

                return Ok(model);
            }

            await this._bookservice.AddBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        [HttpPatch("update/book")]
        public async Task<ActionResult<bool>> UpdateBook(Book book)
        {
            try
            {
                await this._bookservice.UpdateAsync(book);
                return Ok(true);
            }
            catch(Exception)
            {
                return BadRequest(false);
            }
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest("Id not provided");
            }

            Book book = await this._bookservice.FindBookByIdAsync(id);

            if (book == null)
            {
                return NotFound("Book not found!");
            }

            await this._bookservice.RemoveBookAsync(id);
            return Ok(true);
        }



    }
}
