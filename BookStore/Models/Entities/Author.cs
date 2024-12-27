using System;

namespace BookStore.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public string Image { get; set; }
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
