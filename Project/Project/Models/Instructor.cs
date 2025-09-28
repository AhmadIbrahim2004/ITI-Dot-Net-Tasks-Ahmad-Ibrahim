using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public string? Image { get; set; }

        // Foreign Key to the Department table
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
