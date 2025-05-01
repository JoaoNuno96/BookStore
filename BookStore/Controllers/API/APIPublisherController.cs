using Microsoft.AspNetCore.Mvc;
using BookStore.Models.Entities;
using BookStore.Models.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIPublisherController : ControllerBase
    {
        private readonly PublisherService _pubService;
        public APIPublisherController(PublisherService pubserviceParama)
        {
            this._pubService = pubserviceParama;
        }

        //GET ALL
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAllPublishers()
        {
            return await this._pubService.GetPublishersAsync();
        }
    }
}
