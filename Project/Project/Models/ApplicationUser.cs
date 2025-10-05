using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    // This class represents a user in your system.
    public class ApplicationUser : IdentityUser
    {
        // You can add custom properties here in the future if you want.
        // For example:
        // public string? FirstName { get; set; }
        // public string? LastName { get; set; }
    }
}
