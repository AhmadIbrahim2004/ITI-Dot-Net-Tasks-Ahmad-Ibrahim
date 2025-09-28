using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string ManagerName { get; set; }

        // Navigation Properties
        public virtual ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
