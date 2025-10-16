using JwtAuthenticationDotNet.Models;
using JwtAuthenticationDotNet.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JwtAuthenticationDotNet.Extensions.RouteHandlers;
/// <summary>
/// Provides methods for registering API routes for both public and protected endpoints.
/// </summary>
public static class RouteBuilder
{
    /// <summary>
    /// Registers all application routes including public and protected endpoints.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to map routes.</param>
    public static void MapAppRoutes(this WebApplication app)
    {
        MapPublicRoutes(app);
        MapProtectedRoutes(app);
    }

    /// <summary>
    /// Registers public routes such as registration and login.
    /// These routes do not require authentication.
    /// </summary>
    /// <param name="webApplication"></param>
    private static void MapPublicRoutes(this WebApplication webApplication)
    {
        var app = webApplication.MapGroup("").WithTags("Authentication");

        // Register user and return JWT token on success
        app.MapPost("/register", async (
            UserManager<AppUser> userManager,
            TokenService tokenService,
            string username,
            string password,
            string role,
            string platform) => 
        {
            var user = new AppUser { UserName = username };
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) return Results.BadRequest(result.Errors);

            await userManager.AddToRoleAsync(user, role);

            // 🔐 Generate token immediately after successful registration
            var token = await tokenService.GenerateTokenAsync(user, platform);

            return Results.Ok(new { token });
        });

        // Login user and return JWT token
        app.MapPost("/login", async (
            UserManager<AppUser> userManager,
            TokenService tokenService,
            string username,
            string password,
            string platform) =>
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null || !await userManager.CheckPasswordAsync(user, password))
                return Results.Unauthorized();

            var token = await tokenService.GenerateTokenAsync(user, platform);
            return Results.Ok(new { token });
        });
    }

    /// <summary>
    /// Registers protected routes that require authentication or specific roles.
    /// </summary>
    /// <param name="webApplication">The <see cref="WebApplication"/> used to define the routes.</param>
    private static void MapProtectedRoutes(this WebApplication webApplication)
    {
        var app = webApplication.MapGroup("").WithTags("Protected Routes");
        
        app.MapGet("/profile", (ClaimsPrincipal user) =>
        {
            var username = user.Identity?.Name;
            return Results.Ok($"Hello {username}, you're logged in.");
        }).RequireAuthorization();

        // ROLE-BASED ROUTES
        app.MapGet("/admin", () => Results.Ok("Hello Admin"))
            .RequireAuthorization("AdminPolicy");

        app.MapGet("/customer", () => Results.Ok("Hello Customer"))
            .RequireAuthorization("CustomerPolicy");

        app.MapGet("/web", () => Results.Ok("Hello Web User"))
            .RequireAuthorization("WebPolicy");
    }
}
