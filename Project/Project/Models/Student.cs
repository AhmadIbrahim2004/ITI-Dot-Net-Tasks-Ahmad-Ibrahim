using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Url]
        public string? Image { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Range(1, 100)]
        public int Grade { get; set; }

        [Display(Name = "Department")]
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        // --- NEW PROPERTY TO LINK TO THE USER ACCOUNT ---
        [Display(Name = "User Account")]
        public string? ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }


        // Navigation Properties
        public virtual Department? Department { get; set; }
        public virtual ICollection<CourseStudent>? CourseStudents { get; set; }
    }
}
