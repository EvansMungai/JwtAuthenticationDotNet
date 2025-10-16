using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationDotNet.Services;

/// <summary>
/// Provides functionality for seeding default user roles into the system at application startup.
/// </summary>
public static class RoleSeeder
{
    /// <summary>
    /// An array of predefined roles to be created if they do not already exist.
    /// </summary>
    private static readonly string[] Roles = new[]
    {
        "Admin",
        "Customer",
        "Manager",
    };

    /// <summary>
    /// Seeds the predefined roles into the database using the provided service provider.
    /// </summary>
    /// <param name="serviceProvider">The application's service provider used to resolve the <see cref="RoleManager{T}"/> service.</param>
    /// <returns></returns>
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
