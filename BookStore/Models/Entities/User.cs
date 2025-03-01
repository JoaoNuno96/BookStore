using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Entities
{
    public class User : IdentityUser<int>
    {
        [MaxLength(256)]
        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public User() { }
        //public User(string fn, string sa, string city, string st, string pc)
        //{
        //    this.FullName = fn;
        //    this.StreetAddress = sa;
        //    this.City = city;
        //    this.State = st;
        //    this.PostalCode = pc;
        //}
    }
}
