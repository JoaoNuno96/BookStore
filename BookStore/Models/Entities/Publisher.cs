using System.Collections.Generic;

namespace BookStore.Models.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Publisher() { }
        public Publisher(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
