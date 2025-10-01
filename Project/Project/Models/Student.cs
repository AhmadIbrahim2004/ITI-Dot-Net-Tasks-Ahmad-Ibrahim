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

        [Display(Name = "Image URL")]
        [Url]
        public string? Image { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100.")]
        public int Grade { get; set; }

        [Display(Name = "Department")]
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        // Navigation Properties
        public virtual Department? Department { get; set; }
        public virtual ICollection<CourseStudent>? CourseStudents { get; set; }
    }
}
