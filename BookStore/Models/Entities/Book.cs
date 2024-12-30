using BookStore.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} should be between {2} and {1}")]
        public string Name { get; set; }
        public int Pages { get; set; }

        [Display(Name = "Book Measures")]
        [Required(ErrorMessage = "Size Required")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1}")]
        public string Sizes { get; set; }

        [Required(ErrorMessage = "Image Required")]
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public Author Author { get; set; }

        [Required(ErrorMessage = "Category Required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Publisher Required")]
        public int PublisherId { get; set; }

        [Required(ErrorMessage = "Author Required")]
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
