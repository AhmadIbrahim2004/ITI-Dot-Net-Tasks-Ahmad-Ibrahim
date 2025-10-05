using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Implementations
{
    // SRP: This class is solely responsible for data operations related to the Course entity.
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Include related Department and Instructor data when getting all courses
        public IEnumerable<Course> GetAll() => _context.Courses
                                                        .Include(c => c.Department)
                                                        .Include(c => c.Instructor)
                                                        .ToList();

        // Include related data when getting a single course by ID
        public Course? GetById(int id) => _context.Courses
                                                   .Include(c => c.Department)
                                                   .Include(c => c.Instructor)
                                                   .FirstOrDefault(c => c.Id == id);

        public void Add(Course entity) => _context.Courses.Add(entity);

        public void Update(Course entity) => _context.Courses.Update(entity);

        public void Delete(Course entity) => _context.Courses.Remove(entity);

        public void Save() => _context.SaveChanges();
    }
}
