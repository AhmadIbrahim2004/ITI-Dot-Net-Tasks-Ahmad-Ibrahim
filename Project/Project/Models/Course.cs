using Project.ValidationAttributes; // Import your new validation attributes
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [Range(50, 100, ErrorMessage = "Degree must be between 50 and 100.")]
        public int Degree { get; set; }

        [Required]
        [Display(Name = "Minimum Degree")]
        [Range(20, 50, ErrorMessage = "Minimum degree must be between 20 and 50.")]
        // --- THIS IS THE NEW CUSTOM VALIDATION ---
        // It checks the "Degree" property to make sure this value is smaller.
        [MinimumDegreeValidation("Degree")]
        public int MinimumDegree { get; set; }

        [Required]
        [Range(1, 12)]
        public int Hours { get; set; }

        // Foreign Keys
        [Display(Name = "Department")]
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        [Display(Name = "Instructor")]
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }


        // Navigation Properties
        public virtual Department? Department { get; set; }
        public virtual Instructor? Instructor { get; set; }
        public virtual ICollection<CourseStudent>? CourseStudents { get; set; }
    }
}

