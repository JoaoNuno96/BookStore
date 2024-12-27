namespace BookStore.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Category() { }
        public Category(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}
