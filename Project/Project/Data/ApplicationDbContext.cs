using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Data
{
    // This class is now the single source for both your application data and Identity data.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Your application's domain models
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This line is CRUCIAL. It configures the schema needed for the Identity framework.
            // It must be called before your custom configurations.
            base.OnModelCreating(modelBuilder);

            // --- Your Custom Model Configuration ---

            // Configure the composite primary key for the junction table
            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CrsId, cs.StdId });

            // Configure relationships to prevent cascade delete cycles, which SQL Server forbids.
            // Using .OnDelete(DeleteBehavior.Restrict) is a safe way to prevent deletion
            // if related entities exist.
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DeptId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StdId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

