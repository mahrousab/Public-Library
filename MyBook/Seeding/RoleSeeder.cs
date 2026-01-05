using Microsoft.AspNetCore.Identity;
using PublicLibrary.DTOS;

namespace PublicLibrary.Seeding
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[]
            {
            UserRoles.Admin,
            UserRoles.User,
            UserRoles.Publisher,
            UserRoles.Author
        };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
