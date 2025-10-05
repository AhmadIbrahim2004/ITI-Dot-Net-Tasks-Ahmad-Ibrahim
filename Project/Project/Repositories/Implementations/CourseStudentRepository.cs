using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repositories.Interfaces;

namespace Project.Repositories.Implementations
{
    public class CourseStudentRepository : ICourseStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseStudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseStudent> GetAll() => _context.CourseStudents
                                                                .Include(cs => cs.Course)
                                                                .Include(cs => cs.Student)
                                                                .ToList();

        // CourseStudent has a composite key, so GetById needs both keys.
        // For simplicity with the generic interface, we'll find by the first key here.
        // A real app might need a more specific GetById(int courseId, int studentId).
        public CourseStudent? GetById(int id) => _context.CourseStudents.FirstOrDefault(cs => cs.CrsId == id);

        public void Add(CourseStudent entity) => _context.CourseStudents.Add(entity);

        public void Update(CourseStudent entity) => _context.CourseStudents.Update(entity);

        public void Delete(CourseStudent entity) => _context.CourseStudents.Remove(entity);

        public void Save() => _context.SaveChanges();
    }
}
