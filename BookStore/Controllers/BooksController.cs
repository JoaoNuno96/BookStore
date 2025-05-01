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
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly HttpClient _httpClientBook;
        private readonly HttpClient _httpClientCat;
        private readonly HttpClient _httpClientAuth;
        private readonly HttpClient _httpClientPub;

        public BooksController(IHttpClientFactory httpclintB,IHttpClientFactory httpclintC,IHttpClientFactory httpClintA,IHttpClientFactory httpClintP)
        {
            this._httpClientBook = httpclintB.CreateClient("BookApi");
            this._httpClientCat = httpclintC.CreateClient("CategoryApi");
            this._httpClientAuth = httpClintA.CreateClient("AuthorApi");
            this._httpClientPub = httpClintP.CreateClient("PublisherApi");

        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClientBook.GetAsync("getAll");

            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<Book> listbooks = JsonConvert.DeserializeObject<List<Book>>(json);
                return View(listbooks);
            }

            return View(new List<Book>());
        }


        public async Task<IActionResult> Create()
        {
            List<Category> categories = null;
            List<Author> authors = null;
            List<Publisher> pubs = null;

            //CATEGORIES
            using (HttpResponseMessage httpResponse = await _httpClientCat.GetAsync("getAll"))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(json);
                }
            }

            //AUTHORS
            using (HttpResponseMessage httpResponse = await _httpClientAuth.GetAsync("getAll"))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    authors = JsonConvert.DeserializeObject<List<Author>>(json);
                }
            }

            //PUBLISHER
            using (HttpResponseMessage httpResponse = await _httpClientPub.GetAsync("getAll"))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    pubs = JsonConvert.DeserializeObject<List<Publisher>>(json);
                }
            }

            BookFormViewModel viewModelBook = new BookFormViewModel
            {
                Categories = categories,
                Publishers = pubs,
                Authors = authors
            };


            return View(viewModelBook);
        }

        public async Task<IActionResult> Details(int id)
        {
            Book book = null;

            using (HttpResponseMessage response = await _httpClientBook.GetAsync($"details/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(json);
                    return View(book);
                }
                return View(new Book());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            //Caso os dados estejam bem (validação backend)
            if(!ModelState.IsValid)
            {
                HttpResponseMessage response = await this._httpClientBook.GetAsync("getViewModel");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    BookFormViewModel bookViewModel = JsonConvert.DeserializeObject<BookFormViewModel>(json);
                }
                return View(new BookFormViewModel());
            }
            //JSONCONVERT -> ORGANIZE CONTENT WITH JSON -> HTTPRESPONSEMESSAGE
            var jsonContent = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage responsePOST = await _httpClientBook.PostAsync("create/book", content);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "Id not provided!"});
            }

            using (HttpResponseMessage response = await this._httpClientBook.GetAsync($"details/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Book book = JsonConvert.DeserializeObject<Book>(json);

                    return View(book);
                }
                else
                {
                    return RedirectToAction(nameof(Error), new { message = "Book not found!" });
                }
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            using (HttpResponseMessage response = await this._httpClientBook.GetAsync($"remove/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Error), new { message = "Something went wrong! " });
                }
            }

        }

        public async Task<IActionResult> Edit(int? id)
        {
            Book obj = null;
            List<Category> categories = null;
            List<Author> authors = null;
            List<Publisher> pubs = null;

            //Verificar se o id recebido é nulo
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided!" });
            }

            //Verificar se esse id existe na base de dados (PELA API)
            using (HttpResponseMessage htpmessage = await this._httpClientBook.GetAsync($"details/{id}"))
            {
                if (htpmessage.IsSuccessStatusCode)
                {
                    var json = await htpmessage.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Book>(json);
                }
            }

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Book not found!" });
            }

            //CATEGORIES
            using (HttpResponseMessage httpResponse = await _httpClientCat.GetAsync("getAll"))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    categories = JsonConvert.DeserializeObject<List<Category>>(json);
                }
            }

            //AUTHORS
            using (HttpResponseMessage httpResponse = await _httpClientAuth.GetAsync("getAll"))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    authors = JsonConvert.DeserializeObject<List<Author>>(json);
                }
            }

            //PUBLISHER
            using (HttpResponseMessage httpResponse = await _httpClientPub.GetAsync("getAll"))
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var json = await httpResponse.Content.ReadAsStringAsync();
                    pubs = JsonConvert.DeserializeObject<List<Publisher>>(json);
                }
            }

            BookFormViewModel model = new BookFormViewModel
            {
                Book = obj,
                Authors = authors,
                Categories = categories,
                Publishers = pubs
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            List<Category> categories = null;
            List<Author> authors = null;
            List<Publisher> pubs = null;

            if (!ModelState.IsValid)
            {
                //CATEGORIES
                using (HttpResponseMessage httpResponse = await _httpClientCat.GetAsync("getAll"))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var json = await httpResponse.Content.ReadAsStringAsync();
                        categories = JsonConvert.DeserializeObject<List<Category>>(json);
                    }
                }

                //AUTHORS
                using (HttpResponseMessage httpResponse = await _httpClientAuth.GetAsync("getAll"))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var json = await httpResponse.Content.ReadAsStringAsync();
                        authors = JsonConvert.DeserializeObject<List<Author>>(json);
                    }
                }

                //PUBLISHER
                using (HttpResponseMessage httpResponse = await _httpClientPub.GetAsync("getAll"))
                {
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var json = await httpResponse.Content.ReadAsStringAsync();
                        pubs = JsonConvert.DeserializeObject<List<Publisher>>(json);
                    }
                }


                BookFormViewModel model = new BookFormViewModel
                {
                    Book = book,
                    Authors = authors,
                    Categories = categories,
                    Publishers = pubs
                };

                return View(model);
            }

            if (id != book.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not the same as Object passed!" });
            }

            var jsonContent = JsonConvert.SerializeObject(book);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage responsePOST = await _httpClientBook.PostAsync("update/book", content);
            return RedirectToAction(nameof(Index));
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
