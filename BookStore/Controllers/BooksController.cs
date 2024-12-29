using BookStore.Models.Services;
using BookStore.Models.Services.Exceptions;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public IActionResult Index()
        {
            List<Book> listbooks = this._bookservice.FindAllBooks();

            return View(listbooks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Category> listCategories = this._categoryservice.GetCategories();
            List<Author> listAuthors = this._authorservice.GetAuthors();
            List<Publisher> listPublishers = this._publisherservice.GetPublishers();

            BookFormViewModel model = new BookFormViewModel
            {
                Authors = listAuthors,
                Categories = listCategories,
                Publishers = listPublishers
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            this._bookservice.AddBook(book);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided!"});
            }

            Book book = this._bookservice.FindBookById(id.Value);

            if(book == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Book not found!" });
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            this._bookservice.RemoveBook(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //Verificar se o id recebido é nulo
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            //Verificar se esse id existe na base de dados
            Book obj = this._bookservice.FindBookById((int)id.Value);

            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Book not found!" });
            }

            List<Category> listCategories = this._categoryservice.GetCategories();
            List<Author> listAuthors = this._authorservice.GetAuthors();
            List<Publisher> listPublishers = this._publisherservice.GetPublishers();
            Book book = this._bookservice.FindBookById(id.Value);

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
        public IActionResult Edit(int id, Book book)
        {
            if(id != book.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not the same as Object passed!" });
            }

            try
            {
                this._bookservice.Update(book);
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
