using BookStore.Models.Services;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly HttpClient _httpClientAuth;

        public AuthorController(IHttpClientFactory httpclient)
        {
            this._httpClientAuth = httpclient.CreateClient("AuthorApi");
        }

        public async Task<IActionResult> Index()
        {
            List<Author> listAuth = null;
            using (HttpResponseMessage httpmessage = await this._httpClientAuth.GetAsync("getAll"))
            {
                string json = await httpmessage.Content.ReadAsStringAsync();
                listAuth = JsonConvert.DeserializeObject<List<Author>>(json);
            }

            return View(listAuth);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Author auth = null;

            using (HttpResponseMessage http = await this._httpClientAuth.GetAsync($"get/{id}"))
            {
                string json = await http.Content.ReadAsStringAsync();
                auth = JsonConvert.DeserializeObject<Author>(json);
            }

            return View(auth);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author authParam)
        {
            if (!ModelState.IsValid)
            {
                //IF IS NOT VALID RENDES THE MODEL AGAIN
                using(HttpResponseMessage http = await this._httpClientAuth.GetAsync($"get/{id}"))
                {
                    if (!http.IsSuccessStatusCode)
                    {
                        return BadRequest("Failed to fetch author from API.");
                    }

                    string jsonNotValid = await http.Content.ReadAsStringAsync();
                    Author auth = JsonConvert.DeserializeObject<Author>(jsonNotValid);

                    if (auth == null)
                    {
                        return NotFound("Author is null after deserialization.");
                    }


                    return View(auth);
                }
            }

            //SEND UPDATE TROUGH API
            string json = JsonConvert.SerializeObject(authParam);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await this._httpClientAuth.PostAsync("edit/author", content);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            using (HttpResponseMessage http = await this._httpClientAuth.GetAsync($"get/{id}"))
            {
                string jsonNotValid = await http.Content.ReadAsStringAsync();
                Author auth = JsonConvert.DeserializeObject<Author>(jsonNotValid);

                return View(auth);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateP(Author form)
        {
            string json = JsonConvert.SerializeObject(form);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await this._httpClientAuth.PostAsync("create/author", content);
            return RedirectToAction(nameof(Index));
        }
    }
}
