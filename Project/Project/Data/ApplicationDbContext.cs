using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{ 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the composite primary key for the CourseStudent junction table
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CrsId, cs.StdId });

            // FIX #1: This prevents the first cascade error between Course and Department
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DeptId)
                .OnDelete(DeleteBehavior.NoAction);

            // FIX #2: This prevents the new cascade error on the CourseStudents table
            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StdId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

