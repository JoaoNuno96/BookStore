using BookStore.Models.Services;
using BookStore.Models.Entities;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public IActionResult Index()
        {
            List<Book> listbooks = this._bookservice.FindAllBooks();

            return View(listbooks);
        }

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

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int bookId)
        {
            this._bookservice.RemoveBook(bookId);
            return RedirectToAction(nameof(Index));
        }
    }
}
