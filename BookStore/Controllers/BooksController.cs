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

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookservice;
        private readonly AuthorService _authorservice;
        private readonly CategoryService _categoryservice;
        private readonly PublisherService _publisherservice;
        public BooksController(BookService bookservice, AuthorService aus, CategoryService cs, PublisherService ps)
        {
            this._bookservice = bookservice;
            this._authorservice = aus;
            this._categoryservice = cs;
            this._publisherservice = ps;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Book> listbooks = await this._bookservice.FindAllBooksAsync();

            return View(listbooks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Book book = await this._bookservice.FindBookByIdAsync(id);
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            //Caso os dados estejam bem (validação backend)
            if(!ModelState.IsValid)
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

                return View(model);
            }
            await this._bookservice.AddBookAsync(book);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided!"});
            }

            Book book = await this._bookservice.FindBookByIdAsync(id.Value);

            if(book == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Book not found!" });
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this._bookservice.RemoveBookAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //Verificar se o id recebido é nulo
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            //Verificar se esse id existe na base de dados
            Book obj = await this._bookservice.FindBookByIdAsync((int)id.Value);

            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Book not found!" });
            }

            List<Category> listCategories = await this._categoryservice.GetCategoriesAsync();
            List<Author> listAuthors = await this._authorservice.GetAuthorsAsync();
            List<Publisher> listPublishers = await this._publisherservice.GetPublishersAsync();
            Book book = await this._bookservice.FindBookByIdAsync(id.Value);

            BookFormViewModel model = new BookFormViewModel
            {
                Book = book,
                Authors = listAuthors,
                Categories = listCategories,
                Publishers = listPublishers
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                List<Category> listCategories = await this._categoryservice.GetCategoriesAsync();
                List<Author> listAuthors = await this._authorservice.GetAuthorsAsync();
                List<Publisher> listPublishers = await this._publisherservice.GetPublishersAsync();


                BookFormViewModel model = new BookFormViewModel
                {
                    Book = book,
                    Authors = listAuthors,
                    Categories = listCategories,
                    Publishers = listPublishers
                };

                return View(model);
            }

            if (id != book.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not the same as Object passed!" });
            }

            try
            {
                await this._bookservice.UpdateAsync(book);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            
        }

        public IActionResult Error(string message)
        {
            //Apanhar Id interno da requisição
            ErrorViewModel model = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(model);
        }
    }
}
