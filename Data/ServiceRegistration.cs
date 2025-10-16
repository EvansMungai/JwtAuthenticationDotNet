using JwtAuthenticationDotNet.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationDotNet.Data;

public static class ServiceRegistration
{
    public static void ConfigureDataServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryDb"));
    }
}
