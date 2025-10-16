using JwtAuthenticationDotNet.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationDotNet.Data;
/// <summary>
/// Provides extension methods for registering DBContext services to the DI container.
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Configures the application's data services, including the Entity Framework Core <see cref="AppDbContext"/> using an in-memory db provider
    /// </summary>
    /// <param name="services">The service collection to which the data services will be added.</param>
    public static void ConfigureDataServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
    }
}
