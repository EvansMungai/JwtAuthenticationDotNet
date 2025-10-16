using JwtAuthenticationDotNet.Extensions.RouteHandlers;
using JwtAuthenticationDotNet.Services;

namespace JwtAuthenticationDotNet.Extensions.MiddlewareConfiguration;

public static class MiddlewareConfiguration
{
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
