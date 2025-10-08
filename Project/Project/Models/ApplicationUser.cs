using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    // This class represents a user in your system.
    public class ApplicationUser : IdentityUser
    {
        // --- ADD THESE PROPERTIES ---
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
