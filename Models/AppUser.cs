using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationDotNet.Models;

public class AppUser : IdentityUser
{
    public DateTime LastLoginTime { get; set; }
}
