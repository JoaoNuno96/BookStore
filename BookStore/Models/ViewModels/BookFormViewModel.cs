using BookStore.Models.Entities;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    //CLASSE COMPONENTE QUE SERVE PARA RECEBER VARIAS DEPENDENCIAS E ENVIAR PARA FORM DE ADICIONAR LIVROS
    public class BookFormViewModel
    {
        public Book Book { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Publisher> Publishers { get; set; }
    }
}
