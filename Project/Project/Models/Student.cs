using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string? Image { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        public int Grade { get; set; }

        // Foreign Key to the Department table
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new HashSet<CourseStudent>();
    }
}
