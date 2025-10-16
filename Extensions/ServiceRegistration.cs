using JwtAuthenticationDotNet.Data;
using JwtAuthenticationDotNet.Extensions.ServiceHandlers;
using JwtAuthenticationDotNet.Services;

namespace JwtAuthenticationDotNet.Extensions;

public static class ServiceRegistration
{
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
