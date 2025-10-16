using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace JwtAuthenticationDotNet.Extensions.ServiceHandlers;
/// <summary>
/// Provides an extension method to configure Swagger (OpenAPI) documentation, including JWT Bearer authentication support for testing secured endpoints.
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// Registers Swagger/OpenAPI services and configures JWT Bearer security definition for API documentation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add Swagger configuration to.</param>
    public static void ConfigureSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi(); // Optional if you're using NSwag; safe to remove if not needed
        services.AddEndpointsApiExplorer();  // Enables minimal APIs to be discovered by Swagger
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT AUTHENTICAITON API", Description = "Building a JWT authentication server", Version = "v1" });

            // Configure JWT Bearer token support in Swagger UI
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' followed by space and your JWT token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}
