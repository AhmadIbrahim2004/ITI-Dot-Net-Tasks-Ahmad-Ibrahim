using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class CourseStudent
    {
        public int Id { get; set; }

        [Range(0, 100)]
        public int Degree { get; set; }

        [ForeignKey("Course")]
        [Display(Name = "Course")]
        public int CrsId { get; set; }

        [ForeignKey("Student")]
        [Display(Name = "Student")]
        public int StdId { get; set; }

        // Navigation Properties
        public virtual Course? Course { get; set; }
        public virtual Student? Student { get; set; }
    }
}
