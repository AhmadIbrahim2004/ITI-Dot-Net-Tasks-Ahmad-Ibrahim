using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class CourseStudent
    {
        public int Id { get; set; }

        public int Degree { get; set; }

        // Foreign Key for Course
        [ForeignKey("Course")]
        public int CrsId { get; set; }

        // Foreign Key for Student
        [ForeignKey("Student")]
        public int StdId { get; set; }

        // Navigation Properties
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
