using BookStore.Data;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Services
{
    public class PublisherService
    {
        private readonly BookStoreContext _context;
        public PublisherService(BookStoreContext contx)
        {
            this._context = contx;
        }
 
        public async Task<List<Publisher>> GetPublishersAsync()
        {
            return await this._context.Publisher.ToListAsync();
        }
    }
}
