using Microsoft.AspNetCore.Identity;

namespace BookStore.Models.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
