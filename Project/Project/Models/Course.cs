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

        public int Degree { get; set; }

        public int MinimumDegree { get; set; }

        public int Hours { get; set; }

        // Foreign Key to the Department table
        [ForeignKey("Department")]
        public int DeptId { get; set; }

        // Foreign Key to the Instructor table
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        // Navigation Properties
        public virtual Department Department { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ICollection<CourseStudent> CourseStudents { get; set; } = new HashSet<CourseStudent>();
    }
}
