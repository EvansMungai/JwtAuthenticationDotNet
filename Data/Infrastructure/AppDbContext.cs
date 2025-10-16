using JwtAuthenticationDotNet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationDotNet.Data.Infrastructure;
/// <summary>
/// Application database context that integrates with ASP Core Identiy.
/// Manages access to the applications's data using EF Core.
/// </summary>
public class AppDbContext : IdentityDbContext<AppUser>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext" /> class using the specified options
    /// </summary>
    /// <param name="options">The options to be used by the <see cref="AppDbContext"/>.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
