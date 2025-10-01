using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Manager Name")]
        public string ManagerName { get; set; }

        // Navigation Properties
        public virtual ICollection<Instructor>? Instructors { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Course>? Courses { get; set; }
    }
}
