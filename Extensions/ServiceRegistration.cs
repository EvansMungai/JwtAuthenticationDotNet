using JwtAuthenticationDotNet.Data;
using JwtAuthenticationDotNet.Extensions.ServiceHandlers;
using JwtAuthenticationDotNet.Services;

namespace JwtAuthenticationDotNet.Extensions;

/// <summary>
/// Provides extension methods to register all required application services,
/// including database, authentication, authorization, and documentation.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Registers application-level services such as Swagger documentation, database context, authentication/authorization, and custom services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> used to register services for dependency injection.</param>
    /// <param name="configuration">The application's configuration object, used for accessing settings like JWT keys.</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add API Explorer & Swagger for documentation
        services.ConfigureSwaggerDocumentation();

        // Configure DBContext
        services.ConfigureDataServices();

        // Configure Authentication and Authorization services
        services.ConfigureAuthenticationServices(configuration);

        // Register Feature services
        services.AddScoped<TokenService>();
    }
}
