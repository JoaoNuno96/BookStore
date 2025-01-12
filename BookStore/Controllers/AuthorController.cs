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

        public async Task<IActionResult> Edit(int id)
        {
            Author auth = await this._authservice.FindAuthorByIdAsync(id);

            return View(auth);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author auth)
        {
            if(!ModelState.IsValid)
            {
                return View(await this._authservice.FindAuthorByIdAsync(id));
            }

            await this._authservice.EditAuthorAsync(auth);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await this._authservice.FindAuthorByIdAsync(id));
        }

        
    }
}
