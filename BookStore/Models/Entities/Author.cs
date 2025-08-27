using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }

        [Display(Name = "Author's Name")]
        public string Name { get; set; }

        [Display(Name = "BirthDate")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Picture Link")]
        public string Image { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        public Author() { }
        public Author(int id, string name, DateTime bd, string img, string nac)
        {
            this.Id = id;
            this.Name = name;
            this.BirthDay = bd;
            this.Image = img;
            this.Nationality = nac;
        }
    }
}
