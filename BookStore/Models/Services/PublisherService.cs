using BookStore.Data;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Models.Services
{
    public class PublisherService
    {
        private readonly BookStoreContext _context;
        public PublisherService(BookStoreContext contx)
        {
            this._context = contx;
        }
        [HttpGet]
        public List<Publisher> GetPublishers()
        {
            return this._context.Publisher.ToList();
        }
    }
}
