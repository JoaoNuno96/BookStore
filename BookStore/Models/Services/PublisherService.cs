using BookStore.Data;
using BookStore.Models.Entities;
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
        public List<Publisher> GetPublishers()
        {
            return this._context.Publisher.ToList();
        }
    }
}
