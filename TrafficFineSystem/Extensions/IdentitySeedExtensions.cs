using Microsoft.AspNetCore.Identity;
using TrafficFineSystem.Data.Entities;

namespace TrafficFineSystem.Extensions
{
    public static class IdentitySeedExtensions
    {
        public static async Task SeedRolesAsync(this IServiceProvider serviceProvider)
        {
            var roleManager =serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            string[] roles =
            {
                "Manager",
                "Finance"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }

        public static async Task SeedUsersAsync(this IServiceProvider serviceProvider)
        {
            var userManager =serviceProvider .GetRequiredService<UserManager<AppUser>>();

            await CreateUserAsync( userManager, "manager@trafficfine.com", "Manager123","Manager");
            await CreateUserAsync( userManager,"finance@trafficfine.com","Finance123","Finance");
        }

        private static async Task CreateUserAsync(UserManager<AppUser> userManager,string email,string password, string role)
        {
            var user =await userManager.FindByEmailAsync(email);

            if (user is not null)
                return;

            user = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result =await userManager.CreateAsync(user,password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user,role);
            }
        }
    }
}
