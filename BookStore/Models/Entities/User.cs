namespace BookStore.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfPassword { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public User(string fn, string e,string p, string cfp, string sa, string city, string st, string pc)
        {
            this.FullName = fn;
            this.Email = e;
            this.Password = p;
            this.ConfPassword = cfp;
            this.StreetAddress = sa;
            this.City = city;
            this.State = st;
            this.PostalCode = pc;
        }
    }
}
