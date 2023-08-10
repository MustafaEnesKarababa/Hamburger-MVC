using IdentitySonProje.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentitySonProje.Models.Utilities
{
    public static class ForLogin
    {
        public static async void AddSuperUserAsync(UserManager<AppUser> userManager) 
        {
            AppUser user = new AppUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM"

            };
            await userManager.CreateAsync(user, "Admin123.");
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
