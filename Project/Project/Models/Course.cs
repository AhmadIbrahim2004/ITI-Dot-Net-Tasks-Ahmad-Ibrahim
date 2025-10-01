using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(50, 100)]
        public int Degree { get; set; }

        [Display(Name = "Minimum Degree")]
        [Range(0, 100)]
        public int MinimumDegree { get; set; }

        [Range(1, 10, ErrorMessage = "Hours must be between 1 and 10.")]
        public int Hours { get; set; }

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
