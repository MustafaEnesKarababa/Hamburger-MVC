using Microsoft.AspNetCore.Identity;

namespace IdentitySonProje.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? Address { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }    
        public ICollection<Order>? Orders { get; set; }
    }
}
