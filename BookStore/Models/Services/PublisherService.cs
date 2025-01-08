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

        public async Task CreatePublisherAsync(Publisher pub)
        {
            await this._context.Publisher.AddAsync(pub);
            await this._context.SaveChangesAsync();
        }

        public async Task<Publisher> GetPublisherFromIdAsync(int id)
        {
            return await this._context.Publisher.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdatePublisherAsync(Publisher pub)
        {
            this._context.Publisher.Update(pub);
            await this._context.SaveChangesAsync();
        }

        public async Task RemovePublisher(int id)
        {
            Publisher pub = await this.GetPublisherFromIdAsync(id);
            this._context.Publisher.Remove(pub);
            await this._context.SaveChangesAsync();
        }
    }
}
