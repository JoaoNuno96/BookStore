using BookStore.Models.Services;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookservice;
        public BooksController(BookService bookservice)
        {
            _bookservice = bookservice;
        }

        public IActionResult Index()
        {
            List<Book> listbooks = this._bookservice.FindAllBooks();

            return View(listbooks);
        }
    }
}
