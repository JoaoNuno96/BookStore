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
    }
}
