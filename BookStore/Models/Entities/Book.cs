using BookStore.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }

        [Display(Name = "Book Measures")]
        public string Sizes { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public Author Author { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public Book() { }
        public Book(int id, string name, int p, string siz, string img, Category cat, Publisher pub, Author auth)
        {
            this.Id = id;
            this.Name = name;
            this.Pages = p;
            this.Sizes = siz;
            this.ImageUrl = img;
            this.Category = cat;
            this.Publisher = pub;
            this.Author = auth;
        }


    }
}
