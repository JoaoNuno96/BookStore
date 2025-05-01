using BookStore.Models.Entities;
using BookStore.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BookStore.Controllers
{
    public class PublisherController : Controller
    {
        private readonly HttpClient _httpclientPub;

        public PublisherController(IHttpClientFactory httpclientPublisher)
        {
            this._httpclientPub = httpclientPublisher.CreateClient("PublisherApi");
        }

        public async Task<IActionResult> Index()
        {
            List<Publisher> listPub = null;

            using (HttpResponseMessage httpresponse = await this._httpclientPub.GetAsync("getAll"))
            {
                string json = await httpresponse.Content.ReadAsStringAsync();
                listPub = JsonConvert.DeserializeObject<List<Publisher>>(json);
            }

            return View(listPub);
        }
    }
}
