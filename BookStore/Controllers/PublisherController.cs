using BookStore.Models.Entities;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly PublisherService _pubservice;

        public PublisherController(PublisherService ps)
        {
            this._pubservice = ps;
        }

        public async Task<IActionResult> Index()
        {
            List<Publisher> listpub = await this._pubservice.GetPublishersAsync();

            return View(listpub);
        }
    }
}
