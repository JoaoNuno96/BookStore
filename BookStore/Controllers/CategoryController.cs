using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Services;
using BookStore.Models.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;


namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoryController(IHttpClientFactory httpParam)
        {
            this._httpClient = httpParam.CreateClient("CategoryApi");
        }

        public async Task<IActionResult> Index()
        {
            List<Category> categories = null;

            using (HttpResponseMessage httpResp = await this._httpClient.GetAsync("getAll"))
            {
                string json = await httpResp.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<Category>>(json);
            }

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category newEntity)
        {
            var jsonConvert = JsonConvert.SerializeObject(newEntity);
            var content = new StringContent(jsonConvert, Encoding.UTF8, "application/json");

            HttpResponseMessage responsePost = await this._httpClient.PostAsync("create/new/category", content);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Category cat = null;

            using (HttpResponseMessage http = await this._httpClient.GetAsync($"get/{id}"))
            {
                string json = await http.Content.ReadAsStringAsync();
                cat = JsonConvert.DeserializeObject<Category>(json);
            }

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Category form)
        {
            var jsonContent = JsonConvert.SerializeObject(form);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var responde = await this._httpClient.PutAsync($"update/{id}/category",content);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            Category category = null;

            using (HttpResponseMessage httpresponse = await this._httpClient.GetAsync($"get/{id}"))
            {
                var content = await httpresponse.Content.ReadAsStringAsync();
                category = JsonConvert.DeserializeObject<Category>(content);
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Category cat = null;

            using (HttpResponseMessage http = await this._httpClient.GetAsync($"get/{id}"))
            {
                string json = await http.Content.ReadAsStringAsync();
                cat = JsonConvert.DeserializeObject<Category>(json);
            }

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await this._httpClient.DeleteAsync($"remove/{id}");

            return RedirectToAction(nameof(Index));
        }


    }
}
