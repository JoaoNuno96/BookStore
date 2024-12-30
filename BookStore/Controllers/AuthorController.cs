using BookStore.Models.Services;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService _authservice;
        public AuthorController(AuthorService aus)
        {
            this._authservice = aus;
        }

        public async Task<IActionResult> Index()
        {
            List<Author> listAuth = await this._authservice.GetAuthorsAsync();

            return View(listAuth);
        }

        
    }
}
