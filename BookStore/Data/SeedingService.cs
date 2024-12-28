using System;
using Microsoft.EntityFrameworkCore.Internal;
using BookStore.Models.Entities;

namespace BookStore.Data
{
    public class SeedingService
    {
        private readonly BookStoreContext _context;
        public SeedingService(BookStoreContext context)
        {
            _context = context;
        }

        public void Seeding()
        {
            if (this._context.Book.Any() || this._context.Category.Any() || this._context.Author.Any() || this._context.Publisher.Any())
            {
                return;
            }

            //Category
            Category c1 = new Category(1, "Fantasy");

            //Author
            Author a1 = new Author(1, "J. K. Rowling", new DateTime(1965, 7, 31), @"https://www.presenca.pt/cdn/shop/articles/JKR-Children_s-Credit-Debra-Hurford-Brown_net_2358f4ef-d1b5-4701-9060-7aa8c4003b8b_1170x.png?v=1730244262", "Reino Unido");
            Author a2 = new Author(2, "Christopher Paolini", new DateTime(1983, 11, 17), @"https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTSr0ocujsYRx9bteMfO8iTz_kQd0g-L2GibObxBbLByoeVJX7irJK1lM1miG-v2a-IS2wRIvsNeV-Nb-TRYq4Qiw", "Los Angeles");
            Author a3 = new Author(3, "Rick Riordan", new DateTime(1964, 6, 5), @"https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcTk20Oog_xOn8uITza-xT4tneQcwKLeb9mk9uMR2rbxD4HQKytWjkpq0Oo9oYf92N8b3M_2J1xjqkVBR-H5bb2ESA", "Texas");

            //Publisher
            Publisher p1 = new Publisher(1, "Bloomsbury");
            Publisher p2 = new Publisher(2, "Alfred A. Knopf");
            Publisher p3 = new Publisher(3, "Miramax Books");

            //Books Harry Potters - Percy Jackson - Eragon

            //HARRY POTTER
            Book hp1 = new Book(1, "Harry Potter and the Sorcerer's Stone", 223, "50x50", @"https://m.media-amazon.com/images/I/91wKDODkgWL._AC_UF894,1000_QL80_.jpg", c1, p1, a1);
            Book hp2 = new Book(2, "Harry Potter and the Chamber of Secrets", 251, "50x50", @"https://adevoradoradelivros.com.br/wp-content/uploads/2009/08/harry-potter-and-the-chamber-of-secrets_a-devoradora-de-livros.jpg", c1, p1, a1);
            Book hp3 = new Book(3, "Harry Potter and the Prisoner of Azkaban", 317, "50x50", @"https://m.media-amazon.com/images/I/812CcFkEPCL._AC_UF894,1000_QL80_.jpg", c1, p1, a1);
            Book hp4 = new Book(4, "Harry Potter and the Goblet of Fire", 636, "50x50", @"https://cdn.europosters.eu/image/1300/art-photo/harry-potter-goblet-of-fire-book-cover-i214929.jpg", c1, p1, a1);
            Book hp5 = new Book(5, "Harry Potter and the Order of the Phoenix", 766, "50x50", @"https://m.media-amazon.com/images/I/71pgI2ou5oL._AC_UF894,1000_QL80_.jpg", c1, p1, a1);
            Book hp6 = new Book(6, "Harry Potter and the Half-Blood Prince", 607, "50x50", @"https://m.media-amazon.com/images/I/81VsN7EOBfL._AC_UF894,1000_QL80_.jpg", c1, p1, a1);
            Book hp7 = new Book(7, "Harry Potter and the Deathly Hallows", 607, "50x50", @"https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1627042661i/58613224.jpg", c1, p1, a1);

            //THE INHERITANCE CYCLE
            Book ic1 = new Book(8, "Eragon", 544, "50x50", @"https://upload.wikimedia.org/wikipedia/en/c/ce/Eragon_book_cover.png", c1, p2, a2);
            Book ic2 = new Book(9, "Eldest", 694, "50x50", @"https://upload.wikimedia.org/wikipedia/en/e/e0/Eldest_book_cover.png", c1, p2, a2);
            Book ic3 = new Book(10, "Brisingr", 831, "50x50", @"https://upload.wikimedia.org/wikipedia/en/7/70/Brisingr_book_cover.png", c1, p2, a2);
            Book ic4 = new Book(11, "Inheritance", 860, "50x50", @"https://upload.wikimedia.org/wikipedia/en/2/2b/Inheritance2011.JPG", c1, p2, a2);

            //PERCY JACKSON

            Book pj1 = new Book(12, "Percy Jackson & the Olympians - The Lightning Thief", 377, "50x50", @"https://m.media-amazon.com/images/I/91WN6a6F3RL._AC_UF894,1000_QL80_.jpg", c1, p3, a3);
            Book pj2 = new Book(13, "Percy Jackson & the Olympians - The Sea of Monsters", 279, "50x50", @"https://m.media-amazon.com/images/I/91YMTyxpWLL.jpg", c1, p3, a3);
            Book pj3 = new Book(14, "Percy Jackson & the Olympians - The Titan's Curse", 312, "50x50", @"https://pictures.abebooks.com/isbn/9788580575415-uk.jpg", c1, p3, a3);
            Book pj4 = new Book(15, "Percy Jackson & the Olympians - The Battle of the Labyrinth", 361, "50x50", @"https://m.media-amazon.com/images/M/MV5BYWVhN2M5NDAtMjY2Yi00MjYwLWFhOGEtZjFiMWQ0M2UyM2E5XkEyXkFqcGc@._V1_QL75_UY281_CR46,0,190,281_.jpg", c1, p3, a3);
            Book pj5 = new Book(16, "Percy Jackson & the Olympians - The Last Olympian", 381, "50x50", @"https://m.media-amazon.com/images/I/91f6FyULwCL.jpg", c1, p3, a3);

            this._context.Category.Add(c1);
            this._context.Author.AddRange(a1, a2, a3);
            this._context.Publisher.AddRange(p1, p2, p3);
            this._context.Book.AddRange(hp1, hp2, hp3, hp4, hp5, hp6, hp7, ic1, ic2, ic3, ic4, pj1, pj2, pj3, pj4, pj5);

            this._context.SaveChanges();
        }
    }
}
