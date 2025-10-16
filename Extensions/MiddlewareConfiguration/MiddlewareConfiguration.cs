using JwtAuthenticationDotNet.Extensions.RouteHandlers;
using JwtAuthenticationDotNet.Services;

namespace JwtAuthenticationDotNet.Extensions.MiddlewareConfiguration;
/// <summary>
/// Configures middleware and application-level behaviors during application startup.
/// Includes Swagger setup, authentication/authorization, role seeding and route registration.
/// </summary>
public static class MiddlewareConfiguration
{
    /// <summary>
    /// Applies middleware components to the application's request pipeline and seeds initial roles.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to configure the middleware.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task ConfigureMiddlewareAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ARPELLA STORES API V1");
            });
        }

        app.UseAuthentication();
        app.UseAuthorization();

        // ✅ Seed roles before app starts handling requests
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            await RoleSeeder.SeedRolesAsync(serviceProvider);
        }

        // ✅ Directly call static route registration
        app.MapAppRoutes();
    }
}
