using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(5000, 50000, ErrorMessage = "Salary must be between 5000 and 50000.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")] // This line fixes the database warning
        public decimal Salary { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Display(Name = "Image URL")]
        [Url]
        public string? Image { get; set; }

        [Display(Name = "Department")]
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        // Navigation Properties
        public virtual Department? Department { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
