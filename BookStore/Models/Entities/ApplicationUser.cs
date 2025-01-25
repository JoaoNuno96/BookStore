using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
