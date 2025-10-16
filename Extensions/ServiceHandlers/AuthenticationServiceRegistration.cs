using JwtAuthenticationDotNet.Data.Infrastructure;
using JwtAuthenticationDotNet.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace JwtAuthenticationDotNet.Extensions.ServiceHandlers;
/// <summary>
/// Provides extension methods to configure authentication and authorization services, including Identity and JWT Bearer authentication.
/// </summary>
public static class AuthenticationServiceRegistration
{
    /// <summary>
    /// Configures Identity, JWT authentication, and authorization policies.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <param name="configuration">The application <see cref="IConfiguration"/> used to retrieve JWT settings.</param>
    public static void ConfigureAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Identity
        services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // JWT Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtKey = configuration["Jwt:Key"];
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
            };
        });

        // Authorization Policies
        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("WebPolicy", policy => policy.RequireClaim("platform", "Web").RequireRole("Customer"));
            options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
        });
    }
}
