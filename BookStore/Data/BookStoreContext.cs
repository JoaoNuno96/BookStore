using System;
using Microsoft.EntityFrameworkCore;
using BookStore.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<User, IdentityRole<int>,int>
    {
        public BookStoreContext (DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
    }
}
