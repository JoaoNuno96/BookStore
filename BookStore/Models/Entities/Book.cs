using BookStore.Models.Entities;

namespace BookStore.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public string Sizes { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public Author Author { get; set; }
        public Book() { }
        public Book(int id, string name, int p, string siz, Category cat, Publisher pub, Author auth)
        {
            this.Id = id;
            this.Name = name;
            this.Pages = p;
            this.Sizes = siz;
            this.Category = cat;
            this.Publisher = pub;
            this.Author = auth;
        }


    }
}
