using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationDotNet.Models;

/// <summary>
/// Represents an application user with additional profile data beyond the standard IdentityUser fields.
/// </summary>
public class AppUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the date and time the user last logged into the system.
    /// </summary>
    public DateTime LastLoginTime { get; set; }
}
