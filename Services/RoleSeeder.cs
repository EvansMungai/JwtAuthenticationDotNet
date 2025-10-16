using Microsoft.AspNetCore.Identity;
using System.Data;

namespace JwtAuthenticationDotNet.Services;

public static class RoleSeeder
{
    private static readonly string[] Roles = new[]
    {
        "Admin",
        "Customer",
        "Manager",
    };
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in Roles)
        {
            var exists = await roleManager.RoleExistsAsync(role);
            if (!exists)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
